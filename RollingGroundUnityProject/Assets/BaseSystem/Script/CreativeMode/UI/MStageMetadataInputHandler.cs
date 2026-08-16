using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージのメタデータセットのボタン処理を呼び出すクラス
/// </summary>
public class MStageMetadataInputHandler : MonoBehaviour, ISubMenu
{
    private StageMetaDataFactory m_factory = new StageMetaDataFactory();
    private MStageManager m_stageManager;
    private string m_stageName;
    private string m_comment;
    private StageMetaData m_stageMetaData;

    [SerializeField]
    private InputField m_stageNameInputFiel;

    [SerializeField]
    private InputField m_commentInputField;

    private void Start()
    {
        m_stageManager = MStageManager.Instance;
    }

    /// <summary>
    /// ステージ名のセット
    /// </summary>
    public void SetStageName()
    {
        m_stageName = m_stageNameInputFiel.text;
    }

    /// <summary>
    /// コメントのセット
    /// </summary>
    public void SetComment()
    {
        m_comment = m_commentInputField.text;
    }

    public void OpenSubMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseSubMenu()
    {
        gameObject.SetActive(false);
    }

    public string GetSubMenuName()
    {
        return "ステージ情報編集";
    }

    /// <summary>
    /// ステージメタデータのセット
    /// </summary>
    public void CreateStageMetaData()
    {
        m_stageMetaData = m_factory.CreateStageMetaData(m_stageName, "Unknown", "1.0.0", 1, m_comment);
        m_stageManager.SetStageMetaData(m_stageMetaData);
        m_stageManager.SetIsMetaDataInputed(true);

        CloseSubMenu();
    }
}