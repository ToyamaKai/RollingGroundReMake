using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StageExporter
{
    private MStageManager m_stageManager;

    public StageExporter(MStageManager mStageManager)
    {
        m_stageManager = mStageManager;
    }


    public StageData Export(Dictionary<Vector3Int, int> blocks)
    {
        StageData stageData = new();

        // 各種データの入れ込み
        stageData.StageMetaData = m_stageManager.GetStageMetaData();
        stageData.StageSetting = m_stageManager.GetStageSettingData();

        // ブロックをデータに変換
        foreach (var pair in blocks)
        {
            Vector3Int position = pair.Key;
            int blockId = pair.Value;

            BlockSaveData blockSaveData = new()
            {
                Type = BlockType.Normal,
                BlockDataId = blockId,
                Position = new SerializableVector3Int(position),
                //LiftProperty = null // 必要に応じてリフトプロパティを設定
            };

            stageData.Blocks.Add(blockSaveData);
        }

        return stageData;
    }
}
