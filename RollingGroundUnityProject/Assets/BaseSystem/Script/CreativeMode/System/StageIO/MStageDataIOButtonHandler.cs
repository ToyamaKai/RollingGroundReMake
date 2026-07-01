using RollingGround;
using UnityEngine;

/// <summary>
/// ステージデータエクスポートのUIを管理するクラス
/// </summary>
public class MStageDataIOButtonHandler : MonoBehaviour, ISubMenu
{
    [SerializeField]
    private MCreativeModeMenuUIManager m_creativeModeMenuUIManager;

    [SerializeField]
    private MStageMetadataInputHandler m_stageMetadataInputHandler;

    JSONConverter m_JSONConverter;
    MStageManager m_stageManager;

    private void Start()
    {
        m_JSONConverter = new JSONConverter(
            new StageExporter(GameObject.FindFirstObjectByType<MStageManager>()),
            GameObject.FindFirstObjectByType<StageBlockManager>(),
            GameObject.FindFirstObjectByType<MStageBuilder>(),
            GameObject.FindFirstObjectByType<MStageBlockCleaner>()
        );
        m_stageManager = MStageManager.Instance;
    }

    /// <summary>
    /// ステージデータのエクスポート処理
    /// </summary>
    public void OnStageDataExport()
    {
        if (!m_stageManager.GetIsMetaDataInputed())
        {
            m_stageMetadataInputHandler.OpenSubMenu();
        }
        else
        {
            m_JSONConverter.StageJSONConvert();
        }

        CloseSubMenu();
    }

    /// <summary>
    /// ステージデータのインポート処理
    /// </summary>
    /// <param name="dataPath"></param>
    public void OnStageDataImport(string dataPath)
    {
        m_JSONConverter.StageJsonDeserialize(dataPath);
        
        CloseSubMenu();
    }

    /// <summary>
    /// UIを開く処理
    /// </summary>
    public void OpenSubMenu()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// UIを閉じる処理
    /// </summary>
    public void CloseSubMenu()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// サブメニューの名前を取得する処理
    /// </summary>
    /// <returns></returns>
    public string GetSubMenuName()
    {
        return "ステージエクスポート";
    }
}
