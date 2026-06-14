using UnityEngine;

/// <summary>
/// ステージの構築を行うクラス
/// </summary>
public class MStageBuilder : MonoBehaviour
{
    MBlockDatabase m_blockDatabase; // ブロックのデータベース参照
    BlockManipulator m_blockManipulator; // ブロックの設置・削除を行うクラス参照

    private MStageManager m_stageManager;

    void Awake()
    {
        m_blockDatabase = GameObject.FindFirstObjectByType<MBlockDatabase>();
        m_blockManipulator = GameObject.FindFirstObjectByType<BlockManipulator>();
        m_stageManager = MStageManager.Instance;
    }

    /// <summary>
    /// ステージデータからブロックを設置するメソッド
    /// </summary>
    /// <param name="data"></param>
    public void BuildStage(StageData data)
    {
        // 各種ステージ情報をStageManagerにセット
        m_stageManager.SetStageMetaData(data.StageMetaData);
        m_stageManager.SetStageSettingData(data.StageSetting);

        // ブロックの生成
        foreach (var blocks in data.Blocks)
        {
            m_blockManipulator.SetBlock(blocks.Position.ToVector3Int(), m_blockDatabase.GetBlockData((BlockID)blocks.BlockDataId));
        }   
    }

    /// <summary>
    /// TODO
    /// ブロックのプロパティをセットするメソッド
    /// </summary>
    public void SetProperty()
    {

    }
}
