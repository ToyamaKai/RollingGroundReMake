/// <summary>
/// ステージのメタデータをエクスポートするクラス
/// </summary>
public class StageMetaDataFactory
{
    /// <summary>
    /// ステージのメタデータをエクスポートするメソッド
    /// </summary>
    /// <returns></returns>
    public StageMetaData CreateStageMetaData(string stageName, string authorName, string gameVersion, int formatVersion, string comment)
    {
        StageMetaData stageMetaData = new StageMetaData()
        {
            StageName = stageName,
            AuthorName = authorName,
            GameVersion = gameVersion,
            FormatVersion = formatVersion,
            Comment = comment,
            UniqueId = System.Guid.NewGuid().ToString(),
            Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        return stageMetaData;
    }

    // そのうちバリデーションチェックをいれるかも
}
