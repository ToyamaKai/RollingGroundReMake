using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ステージのDTO(データ転送オブジェクト)
/// </summary>

[Serializable]
public class StageDTO 
{
    public string title;            // ステージタイトル 
    public int version;            // フォーマットバージョン
    public List<BlockDTO> blocks;   // ブロック情報リスト
}
