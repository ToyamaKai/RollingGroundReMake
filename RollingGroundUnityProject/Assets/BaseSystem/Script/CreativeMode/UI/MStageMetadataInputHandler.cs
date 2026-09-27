using RollingGround;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージメタデータ入力ハンドラークラス
/// </summary>
public class MStageMetadataInputHandler : MonoBehaviour, ISubMenu
{
    [SerializeField]
    private MCreativeModeMenuUIManager m_creativeModeMenuUIManager;

    [SerializeField]
    private InputField m_stageNameInputField;   // ステージ名入力フィールド

    [SerializeField]
    private InputField m_commentInputField;     // ステージコメント入力フィールド

    private StageMetadataInputHandler m_stageMetadataInputHandler = new StageMetadataInputHandler();

    private string m_stageName; // ステージ名
    private string m_comment;   // ステージコメント

    public void Start()
    {
        m_stageMetadataInputHandler.Initialize();
        CloseSubMenu();
    }

    /// <summary>
    /// ステージ名のセット
    /// </summary>
    public void SetStageName()
    {
        m_stageName = m_stageNameInputField.text;
    }

    /// <summary>
    /// コメントのセット
    /// </summary>
    public void SetComment()
    {
        m_comment = m_commentInputField.text;
    }

    /// <summary>
    /// ステージ情報を取得してUIに反映
    /// </summary>
    public void GetStageInfoData()
    {
        m_stageMetadataInputHandler.GetStageInfoData(out m_stageName, out m_comment);
        m_stageNameInputField.text = m_stageName;
        m_commentInputField.text = m_comment;
    }

    /// <summary>
    /// ステージメタデータのセット
    /// </summary>
    public void CreateStageMetaData()
    {
        m_stageMetadataInputHandler.SetStageInfoData(m_stageName, m_comment);

        CloseSubMenu();
        m_creativeModeMenuUIManager.ReturnToPreviousSubMenu();
    }

    #region ISubMenuインターフェースの実装

    public void OpenSubMenu()
    {
        GetStageInfoData();
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

    public MenuType Type => MenuType.StageInfo;

    #endregion
}