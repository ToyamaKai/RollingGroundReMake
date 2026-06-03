using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StageExporter
{
    public StageData Export(Dictionary<Vector3Int, int> blocks)
    {
        StageData stageData = new();

        // メタデータ
        stageData.StageMetaData = new StageMetaData()
        {
            StageName = "New Stage",
            AuthorName = "Unknown",
            GameVersion = "1.0",
            FormatVersion = 1,
            Comment = "This is a new stage.",
            UniqueId = System.Guid.NewGuid().ToString(),
            Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // ブロックをデータに変換
        foreach (var pair in blocks)
        {
            Vector3Int position = pair.Key;
            int blockId = pair.Value;

            BlockSaveData blockSaveData = new()
            {
                Type = BlockType.Normal,
                BlockID = blockId,
                Position = new SerializableVector3Int(position),
                //LiftProperty = null // 必要に応じてリフトプロパティを設定
            };

            stageData.Blocks.Add(blockSaveData);
        }

        return stageData;
    }
}
