using UnityEngine;

/// <summary>
/// ステージの構築を行うクラス
/// </summary>
public class MStageBuilder : MonoBehaviour
{
    MBlockDatabase m_blockDatabase; // ブロックのデータベース参照
    BlockManipulator m_blockManipulator; // ブロックの設置・削除を行うクラス参照

    void Awake()
    {
        m_blockDatabase = GameObject.FindFirstObjectByType<MBlockDatabase>();
        m_blockManipulator = GameObject.FindFirstObjectByType<BlockManipulator>();
    }

    public void BuildStage(StageData data)
    {
        Debug.Log(m_blockDatabase);
        foreach (var blocks in data.Blocks)
        {
            m_blockManipulator.SetBlock(blocks.Position.ToVector3Int(), m_blockDatabase.GetBlockData((BlockID)blocks.BlockID));
        }   
    }
}
