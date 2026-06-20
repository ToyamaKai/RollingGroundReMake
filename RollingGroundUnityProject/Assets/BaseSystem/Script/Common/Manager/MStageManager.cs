using UnityEngine;

public class MStageManager : SingletonMonoBehaviour<MStageManager>
{
    // ステージメタデータ
    StageMetaData m_stageMetaData;

    // ステージ設定データ
    StageSettingData m_stageSettingData;

    // セーブ済みかの判定フラグ
    private bool m_isSaved;

    #region ゲッターセッター類
    /// <summary>
    /// ステージデータのゲッター
    /// </summary>
    /// <returns></returns>
    public StageMetaData GetStageMetaData()
    {
        return m_stageMetaData;
    }

    /// <summary>
    /// ステージデータのセッター
    /// </summary>
    /// <param name="stageMetaData"></param>
    public void SetStageMetaData(StageMetaData stageMetaData)
    {
        m_stageMetaData = stageMetaData;
    }

    /// <summary>
    /// ステージセッティングデータのゲッター
    /// </summary>
    /// <returns></returns>
    public StageSettingData GetStageSettingData()
    {
        return m_stageSettingData;
    }

    /// <summary>
    /// ステージセッティングデータのセッター
    /// </summary>
    /// <param name="stageSettingData"></param>
    public void SetStageSettingData(StageSettingData stageSettingData)
    {
        m_stageSettingData = stageSettingData;
    }

    /// <summary>
    /// セーブ済みフラグのゲッター
    /// </summary>
    /// <returns></returns>
    public bool GetIsSaved()
    {
        return m_isSaved;
    }

    /// <summary>
    /// セーブ済みフラグのセッター
    /// </summary>
    /// <param name="isSaved"></param>
    public void SetIsSaved(bool isSaved)
    {
        m_isSaved = isSaved;
    }
    #endregion
}
