using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StageExporter
{
    public StageData Export(StageMetaData stageMetaData, Dictionary<Vector3Int, int> blocks)
    {
        StageData stageData = new();

        stageData.StageMetaData = stageMetaData;

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
