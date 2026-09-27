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
    /// ステージ名とコメントを取得するメソッド
    /// </summary>
    /// <param name="stageName"></param>
    /// <param name="comment"></param>
    public void GetStageInfoData(out string stageName, out string comment)
    {
        if(m_stageManager != null && m_stageManager.GetIsMetaDataInputed())
        {
            stageName   = m_stageManager.GetStageMetaData().StageName;
            comment     = m_stageManager.GetStageMetaData().Comment;
        }
        else
        {
            stageName   = string.Empty;
            comment     = string.Empty;
        }
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
