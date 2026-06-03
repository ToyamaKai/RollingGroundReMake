using System;

/// <summary>
/// ステージのメタデータクラス
/// </summary>
[Serializable]
public class StageMetaData
{
    public string StageName;
    public string AuthorName;
    public string GameVersion;
    public int FormatVersion;
    public string Comment;
    public string UniqueId;
    public string Date;
}