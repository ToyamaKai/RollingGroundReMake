/// <summary>
/// ステージ名とステージコメントをメタデータにセットするクラス
/// </summary>
public class StageMetadataInputHandler
{
    private MStageManager m_stageManager;

    public void Initialize()
    {
        m_stageManager  = MStageManager.Instance;
    }

    /// <summary>
    /// ステージ名とコメントをセットするメソッド
    /// </summary>
    /// <param name="stageName"></param>
    /// <param name="comment"></param>
    public void SetStageInfoData(string stageName, string comment)
    {
        m_stageManager.SetStageInfoData(stageName, comment);
        m_stageManager.SetIsMetaDataInputed(true);
    }
}
