using RollingGround;
using UnityEngine;

/// <summary>
/// ステージ保存、読み込みのボタンの処理クラス
/// </summary>
public class StageDataIOButtonHandler : MonoBehaviour
{
    JSONConverter m_JSONConverter;
    MCreativeModeMenuUIManager m_creativeModeMenuUIManager;
    MStageManager m_stageManager;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize(JSONConverter jsonConverter, MCreativeModeMenuUIManager creativeModeMenuUIManager)
    {
        m_JSONConverter             = jsonConverter;
        m_creativeModeMenuUIManager = creativeModeMenuUIManager;
        m_stageManager              = MStageManager.Instance;
    }

    /// <summary>
    /// ステージのエクスポート処理
    /// </summary>
    public void StageExport()
    {
        if (m_stageManager.GetIsMetaDataInputed())
        {
            m_JSONConverter.StageJSONConvert();
            m_creativeModeMenuUIManager.CloseMenu();
        }
        else
        {
            m_creativeModeMenuUIManager.CloseSubMenuWithReturn(MenuType.StageInfo, MenuType.StageExport);
        }
    }

    /// <summary>
    /// ステージのインポート処理
    /// </summary>
    public void StageImport(string dataPath)
    {
        m_JSONConverter.StageJsonDeserialize(dataPath);
    }
}
