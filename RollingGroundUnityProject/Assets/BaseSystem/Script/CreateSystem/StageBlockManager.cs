using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージのブロックID・座標の管理スクリプト
/// </summary>
public class StageBlockManager : MonoBehaviour
{
    Dictionary<Vector3Int, int> m_blockMap = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ブロックのID, 座標情報を登録
    /// </summary>
    public void RegisterBlock(Vector3Int blockPosition, int blockID)
    {
        m_blockMap.Add(blockPosition, blockID);
    }

    /// <summary>
    /// ブロック座標を元に削除
    /// </summary>
    /// <param name="blockPosition"></param>
    public void RemoveBlock(Vector3Int blockPosition)
    {
        m_blockMap.Remove(blockPosition);
    }

    /// <summary>
    /// 既に同じ座標にブロックが存在するかどうかの判定
    /// </summary>
    /// <param name="blockPosition"></param>
    /// <returns></returns>
    public bool IsBlockOccupied(Vector3 blockPosition)
    {
        Vector3Int blockIntPosition = new Vector3Int((int)blockPosition.x, (int)blockPosition.y, (int)blockPosition.z);
        return m_blockMap.ContainsKey(blockIntPosition);
    }
}
