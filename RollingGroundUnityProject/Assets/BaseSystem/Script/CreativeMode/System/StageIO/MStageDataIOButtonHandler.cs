using RollingGround;
using System;
using UnityEngine;

/// <summary>
/// ステージデータエクスポートのUIを管理するクラス
/// </summary>  
public class MStageDataIOButtonHandler : MonoBehaviour, ISubMenu
{
    [SerializeField]
    private MCreativeModeMenuUIManager m_creativeModeMenuUIManager;

    JSONConverter               m_JSONConverter;
    StageDataIOButtonHandler    m_stageDataIOButtonHandler;

    private void Start()
    {
        m_JSONConverter = new JSONConverter(
            new StageExporter(GameObject.FindFirstObjectByType<MStageManager>()),
            GameObject.FindFirstObjectByType<StageBlockManager>(),
            GameObject.FindFirstObjectByType<MStageBuilder>(),
            GameObject.FindFirstObjectByType<MStageBlockCleaner>()
        );      

        m_stageDataIOButtonHandler = new StageDataIOButtonHandler();
        m_stageDataIOButtonHandler.Initialize(m_JSONConverter, m_creativeModeMenuUIManager);

        CloseSubMenu();
    }

    /// <summary>
    /// エクスポートボタン処理(ステージデータのエクスポート)
    /// </summary>
    public void OnStageDataExport()
    {
        m_stageDataIOButtonHandler.StageExport();
        CloseSubMenu();
    }

    /// <summary>
    /// ステージインポートボタン処理(ステージデータのインポート処理)
    /// </summary>
    /// <param name="dataPath"></param>
    public void OnStageDataImport(string dataPath)
    {
        m_stageDataIOButtonHandler.StageImport(dataPath);

        m_creativeModeMenuUIManager.CloseMenu();
        CloseSubMenu();
    }

    #region ISubMenuの実装

    public MenuType Type => MenuType.StageExport;

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
    #endregion
}