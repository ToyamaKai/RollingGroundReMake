using UnityEngine;
using UnityEngine.UI;

public class MStageMetaDataUIManager : MonoBehaviour
{
    private StageMetaDataFactory m_factory = new StageMetaDataFactory();
    private MStageManager m_stageManager;
    private string m_stageName;
    private string m_comment;

    [SerializeField]
    private InputField m_stageNameInputFiel;

    [SerializeField]
    private InputField m_commentInputField;

    private void Awake()
    {
        m_stageManager = MStageManager.Instance;
    }

    public void SetStageName()
    {
        m_stageName = m_stageNameInputFiel.text;
    }

    public void SetComment()
    {
        m_comment = m_commentInputField.text;
    }

    /// <summary>
    /// ステージメタデータのセット
    /// </summary>
    public void CreateStageMetaData()
    {
        StageMetaData stageMetaData = m_factory.CreateStageMetaData(m_stageName, "Unknown", "1.0.0", 1, m_comment);
        m_stageManager.SetStageMetaData(stageMetaData);

        //Debug.Log($"Stage Meta Data Created: {stageMetaData.StageName}, {stageMetaData.Comment}");
    }
}
