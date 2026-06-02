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
    public Vector3Int Position;
    public LiftPropertyData LiftProperty = null;
}
