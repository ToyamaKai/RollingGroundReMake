using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの(DTO)データ転送オブジェクト
/// </summary>
[Serializable]
public class BlockDTO
{
    public int id;
    public int x, y, z;
    public int rot;
    public ushort flags;
    public Dictionary<string, string> meta;
}
