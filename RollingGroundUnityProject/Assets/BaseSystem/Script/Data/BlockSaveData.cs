using System;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// ブロックのセーブデータクラス
/// </summary>
[Serializable]
public class BlockSaveData
{
    public BlockType Type;
    public int BlockID;
    public SerializableVector3Int Position;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public LiftPropertyData LiftProperty = null;
}
