using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージのメタデータセットのボタン処理を呼び出すクラス
/// </summary>
public class MStageMetadataInputHandler : MonoBehaviour
{
    private StageMetaDataFactory m_factory = new StageMetaDataFactory();
    private MStageManager m_stageManager;
    private string m_stageName;
    private string m_comment;
    private Action m_stageDataExport;

    [SerializeField]
    private InputField m_stageNameInputFiel;

    [SerializeField]
    private InputField m_commentInputField;

    private void Start()
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

    public void SetMetaDataInputUIActive(bool isActive, Action stageDataExport = null)
    {
        gameObject.SetActive(isActive);

        if(isActive)
        {
            m_stageDataExport = stageDataExport;
        }
        else
        {
            m_stageDataExport = null;
        }
    }

    /// <summary>
    /// ステージメタデータのセット
    /// </summary>
    public void CreateStageMetaData()
    {
        StageMetaData stageMetaData = m_factory.CreateStageMetaData(m_stageName, "Unknown", "1.0.0", 1, m_comment);
        m_stageManager.SetStageMetaData(stageMetaData);
        m_stageManager.SetIsSaved(true);
        var callback = m_stageDataExport;
        m_stageDataExport = null;
        gameObject.SetActive(false);
        callback.Invoke();
    }
}