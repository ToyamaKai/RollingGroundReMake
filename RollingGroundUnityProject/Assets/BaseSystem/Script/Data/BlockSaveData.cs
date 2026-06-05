using System;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// ブロックのセーブデータクラス
/// </summary>
[Serializable]
public class BlockSaveData
{
    public BlockType Type;  // ブロックの種類
    public int BlockDataId; // ブロックID(データベース)
    public SerializableVector3Int Position; // 保存用ブロック位置データ

    // リフトプロパティは必要な場合のみ保存するため、Null値を許容する
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public LiftPropertyData LiftProperty = null;
}
