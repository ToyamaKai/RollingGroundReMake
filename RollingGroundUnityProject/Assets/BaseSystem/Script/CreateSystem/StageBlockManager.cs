using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージのブロックID・座標の管理スクリプト
/// </summary>
public class StageBlockManager : MonoBehaviour
{
    Dictionary<Vector3Int, int>         m_blockTypeMap = new();
    Dictionary<Vector3Int, GameObject>  m_blockObjectMap = new();

    /// <summary>
    /// ブロックのID, 座標情報を登録
    /// </summary>
    public void RegisterBlock(Vector3Int blockPosition, int blockID, GameObject cubeObject)
    {
        m_blockTypeMap.Add(blockPosition, blockID);
        m_blockObjectMap.Add(blockPosition, cubeObject);
    }

    /// <summary>
    /// ブロック座標を元に削除
    /// </summary>
    /// <param name="blockPosition"></param>
    public void RemoveBlock(Vector3Int blockPosition)
    {
        GameObject.Destroy(m_blockObjectMap[blockPosition]);
        m_blockTypeMap.Remove(blockPosition);
        m_blockObjectMap.Remove(blockPosition);
    }

    /// <summary>
    /// 既に同じ座標にブロックが存在するかどうかの判定
    /// </summary>
    /// <param name="blockPosition"></param>
    /// <returns></returns>
    public bool IsBlockOccupied(Vector3 blockPosition)
    {
        Vector3Int blockIntPosition = new Vector3Int((int)blockPosition.x, (int)blockPosition.y, (int)blockPosition.z);
        return m_blockTypeMap.ContainsKey(blockIntPosition);
    }

    public Dictionary<Vector3Int, int> GetBlockTypeMap()
    {
        return m_blockTypeMap;
    }
}
