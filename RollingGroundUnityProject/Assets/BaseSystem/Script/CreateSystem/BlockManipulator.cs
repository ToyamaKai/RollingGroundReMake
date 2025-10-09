using RollingGround;
using UnityEngine;

/// <summary>
/// ブロックの設置・削除を行う機能の実装
/// </summary>
public class BlockManipulator : MonoBehaviour
{
    private float m_targetY = 0f;
    private Camera mainCamera;
    private Vector3 m_prePosition;
    private GameObject m_previewBlock;
    private const int m_blockID = 01;
    private StageBlockManager m_stageBlockManager;

    //マウスポインターからレイキャストを飛ばし、指定したY座標に到達した際にX, Z座標の数値を四捨五入し、整数に丸め込む。
    //Y座標はspaceで+1, Lshift or Lctrlで-1. Planeも連動して上下する。
    //あと設置場所を見やすいように半透明でブロックをおす
    //プレイヤーの事を考え、四則演算(丸め込み)方式とレイキャスト方式を用意し、切り替えられるようにする。

    private void Start()
    {
        m_stageBlockManager = GameObject.FindFirstObjectByType<StageBlockManager>();
    }

    private void Update()
    {
        PreviewBlock();
    }

    /// <summary>
    /// 丸め込んだ座標の取得
    /// </summary>
    /// <returns></returns>
    private Vector3 GetSnappedPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float t = (m_targetY - ray.origin.y) / ray.direction.y;
        if (ray.direction.y == 0f || t < 0f)
        {
            return Vector3.zero;
        }

        Vector3 hitPoint = ray.origin + ray.direction * t;

        Vector3 snapped = new Vector3(Mathf.Round(hitPoint.x), m_targetY, Mathf.Round(hitPoint.z));
        return snapped;
    }

    /// <summary>
    /// ブロックの生成
    /// </summary>
    private void PreviewBlock()
    {
        Vector3 nowMousePosition = GetSnappedPoint();
        if(m_prePosition != nowMousePosition)
        {
            //もし前とポジションが違うなら生成したやつを消して生成しなおしやね
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
        if(m_stageBlockManager.IsBlockOccupied(m_prePosition))
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
    }
}
