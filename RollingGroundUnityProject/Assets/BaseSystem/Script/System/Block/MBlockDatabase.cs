using UnityEngine;

/// <summary>
/// ブロックのデータベースを管理するクラス
/// </summary>
public class MBlockDatabase : MonoBehaviour
{
    [SerializeField]
    private BlockData[] blockDatas;

    public BlockData GetBlockData(BlockID id)
    {
        foreach (var blockData in blockDatas)
        {
            if (blockData.id == id)
            {
                return blockData;
            }
        }
        return null;
    }
}
