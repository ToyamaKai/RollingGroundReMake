using RollingGround;
using UnityEngine;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// ブロックの設置・削除を行う機能の実装
/// </summary>
public class BlockManipulator : MonoBehaviour, IInputReceiver
{
    MGameInputManager m_gameInputManager;

    private float m_targetY = 0f;
    private Camera mainCamera;
    private Vector3 m_prePosition;
    private GameObject m_previewBlock;
    private const int m_blockID = 01;
    private StageBlockManager m_stageBlockManager;
    private List<GameObject> m_BlockObjects = new List<GameObject>();
    private float scrollAccumulator = 0f;
    private const float scrollThreshold = 5.0f;
    private Vector2 m_lastMousePosition;
    private Vector3 m_lastSnapped;

    //マウスポインターからレイキャストを飛ばし、指定したY座標に到達した際にX, Z座標の数値を四捨五入し、整数に丸め込む。
    //Y座標はspaceで+1, Lshift or Lctrlで-1. Planeも連動して上下する。
    //あと設置場所を見やすいように半透明でブロックをおす
    //プレイヤーの事を考え、四則演算(丸め込み)方式とレイキャスト方式を用意し、切り替えられるようにする。

    private void Awake()
    {
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_gameInputManager.AddRecieveObject(this);
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

    //TODO:なんかAddressableでロードしてるのが無駄、これをScriptableObjectから生成するようにしないといけない
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
        Ray ray = Camera.main.ScreenPointToRay(currentMousePosition);

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
    /// ブロックの生成
    /// </summary>
    private void PreviewBlock()
    {
        Vector3 nowMousePosition = GetSnappedPoint();
        if(m_prePosition != nowMousePosition)
        {
            Destroy(m_previewBlock);
            m_previewBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
            m_previewBlock.transform.position = nowMousePosition;
        }
        m_prePosition = nowMousePosition;
    }

    /// <summary>
    /// ブロックのセット
    /// </summary>
    private void SetBlock()
    {
        if(!m_stageBlockManager.IsBlockOccupied(m_prePosition))
        {
            GameObject instance = Instantiate(m_BlockObjects[0], m_prePosition, Quaternion.identity);
            m_stageBlockManager.RegisterBlock(new Vector3Int((int)m_prePosition.x, (int)m_prePosition.y, (int)m_prePosition.z), 0, instance);

            instance.name = $"{m_BlockObjects[0]}_Instance";

            Debug.Log($"Prefab SampleBlock を生成しました");
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
            SetBlock();
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
    public void OnMoveUpDown(InputAction.CallbackContext context)
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
