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

/// <summary>
/// Vector3Intをシリアライズ可能な形式で保存するためのクラス
/// </summary>
public class SerializableVector3Int
{
    public int X;
    public int Y;
    public int Z;

    public SerializableVector3Int(Vector3Int pos)
    {
        X = pos.x;
        Y = pos.y;
        Z = pos.z;
    }
}
