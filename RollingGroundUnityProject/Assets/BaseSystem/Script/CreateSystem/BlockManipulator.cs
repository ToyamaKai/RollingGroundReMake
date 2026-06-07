using RollingGround;
using UnityEngine;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// ブロックの設置・削除を行う機能の実装
/// TODO: 実処理と実装の分離
/// </summary>
public class BlockManipulator : MonoBehaviour, IInputReceiver
{
    [SerializeField]
    private Transform m_stageRoot; // 生成するブロックの親オブジェクトのTransform

    MGameInputManager m_gameInputManager; // 入力管理クラス参照
    MBlockDatabase m_blockDatabase; // ブロックのデータベース参照
    BlockHotbar m_blockHotbar; // ホットバーの情報を取得するためのクラス参照
    StageBlockManager m_stageBlockManager; // ステージのブロックID・座標の管理スクリプト

    private Camera m_mainCamera; // マウス位置からワールド座標を計算するためのカメラ参照
    private GameObject m_previewBlock; // ブロックの設置位置をプレビューするためのオブジェクト
    private List<GameObject> m_BlockObjects = new List<GameObject>();
    private const int m_blockID = 01; // ブロックのIDを定数で定義（仮）TODO: ブロックIDの管理方法を考える必要がある
    private const float scrollThreshold = 5.0f; // スクロールの閾値を定義する定数
    private float scrollAccumulator = 0f; // スクロールの累積値を管理する変数
    private float m_targetY = 0f; // ブロックの設置高さを管理する変数
    private Vector3 m_prePosition; // ブロックの設置前の座標を保持する変数
    private Vector2 m_lastMousePosition; // マウスの前回位置を保持する変数
    private Vector3 m_lastSnapped; // 前回の丸め込んだ座標を保持する変数

    private void Awake()
    {
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_gameInputManager.AddRecieveObject(this);
        m_blockDatabase = GameObject.FindFirstObjectByType<MBlockDatabase>();
        m_blockHotbar = GameObject.FindFirstObjectByType<BlockHotbar>();
        m_mainCamera = Camera.main;
    }

    private void Start()
    {
        m_stageBlockManager = GameObject.FindFirstObjectByType<StageBlockManager>();
        State();
    }

    private void Update()
    {
        PreviewBlock();
    }

    //TODO: Addressableでロードしてるのが無駄、これをScriptableObjectから生成するようにしないといけない
    private async void State()
    {
        try
        {
            GameObject prefab = await AddressableLoader.Instance.LoadAsync("SampleBlock");
            m_BlockObjects.Add(prefab);
            for(int i = 0; i < m_BlockObjects.Count; i++)
            {
                Debug.Log(i);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Prefabロード失敗: {e.Message}");
        }
    }

    /// <summary>
    /// 丸め込んだ座標の取得
    /// </summary>
    /// <returns></returns>
    private Vector3 GetSnappedPoint()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        Ray ray = m_mainCamera.ScreenPointToRay(currentMousePosition);

        // マウスが動いていなければX,Zは前回値を使いYだけ更新
        if (currentMousePosition == m_lastMousePosition)
        {
            m_lastSnapped.y = m_targetY;
            return m_lastSnapped;
        }

        // レイキャストで新しい座標を計算
        float t = (m_targetY - ray.origin.y) / ray.direction.y;
        if (ray.direction.y == 0f || t < 0f)
        {
            return Vector3.zero;
        }

        Vector3 hitPoint = ray.origin + ray.direction * t;
        Vector3 snapped = new Vector3(Mathf.Round(hitPoint.x), m_targetY, Mathf.Round(hitPoint.z));

        // 前回値を更新
        m_lastMousePosition = currentMousePosition;
        m_lastSnapped = snapped;

        return snapped;
    }

    #region ブロック関連

    /// <summary>
    /// プレビューブロックの生成
    /// </summary>
    private void PreviewBlock()
    {
        Vector3 nowMousePosition = GetSnappedPoint();

        if (m_previewBlock == null)
        {
            m_previewBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(m_previewBlock.GetComponent<Collider>());
        }

        if(m_prePosition != nowMousePosition)
        {
            m_previewBlock.transform.position = nowMousePosition;
            m_prePosition = nowMousePosition;
        }
    }

    /// <summary>
    /// ブロックのセット
    /// </summary>
    public void SetBlock(Vector3Int position, BlockData blockdata)
    {
        if(!m_stageBlockManager.IsBlockOccupied(position))
        {
            GameObject prefab = blockdata.prefab;

            if (prefab != null)
            {
                GameObject instance = Instantiate(prefab, position, Quaternion.identity.normalized, m_stageRoot);
                m_stageBlockManager.RegisterBlock(position, (int)blockdata.id, instance);

                prefab.name = $"{m_BlockObjects[0]}_Instance";

                Debug.Log($"Prefab SampleBlock を生成しました");
            }
            else
            {
                Debug.Log("ブロックはありません");
            }
        }
        else
        {
            Debug.Log("既に設置されています");
        }
    }

    /// <summary>
    /// ブロックの削除
    /// </summary>
    private void DeleteBlock()
    {
        if(m_stageBlockManager.IsBlockOccupied(m_prePosition))
        {
            m_stageBlockManager.RemoveBlock(new Vector3Int((int)m_prePosition.x, (int)m_prePosition.y, (int)m_prePosition.z));
        }
    }

    #endregion


    #region Input Action関連

    /// <summary>
    /// ブロックの設置
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnBlockSet(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SetBlock(Vector3Int.RoundToInt(m_prePosition), m_blockHotbar.GetSelectedBlockData());
        }
    }

    /// <summary>
    /// ブロックの削除
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnDeleteBlock(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            DeleteBlock();
        }
    }

    ///<summary>
    ///ブロック設置Y座標の上下移動
    /// </summary>
    public void OnBlockHeightChange(InputAction.CallbackContext context)
    {
        float scrollY = context.ReadValue<Vector2>().y;

        scrollAccumulator += scrollY;

        if(scrollAccumulator >= scrollThreshold)
        {
            m_targetY++;
            scrollAccumulator -= scrollThreshold;
        }
        else if (scrollAccumulator <= -scrollThreshold)
        {
            m_targetY--;
            scrollAccumulator += scrollThreshold;
        }
    }
    #endregion
}