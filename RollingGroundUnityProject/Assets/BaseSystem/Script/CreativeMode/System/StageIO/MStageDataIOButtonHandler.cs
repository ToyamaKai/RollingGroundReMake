using RollingGround;
using UnityEngine;

/// <summary>
/// メタデータのボタン処理クラス
/// </summary>
public class MStageDataIOButtonHandler : MonoBehaviour, ISubMenu
{
    [SerializeField]
    private MCreativeModeMenuUIManager m_creativeModeMenuUIManager;

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

    public void OnStageDataExport()
    {
        if (!m_stageManager.GetIsMetaDataInputed())
        {
            // メタデータが入力されていない場合は、先にステージメタデータの入力を促す
            //m_creativeModeMenuUIManager.SetStageMetaDataUIActive(true, OnStageDataExport);
        }
        else
        {
            m_JSONConverter.StageJSONConvert();
        }

        SetStageDataIOUIActive(false);
    }

    public void OnStageDataImport(string dataPath)
    {
        m_JSONConverter.StageJsonDeserialize(dataPath);
        gameObject.SetActive(false);
    }
    
    public void SetStageDataIOUIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void OpenSubMenu()
    {
        SetStageDataIOUIActive(true);
    }

    public void CloseSubMenu()
    {
        SetStageDataIOUIActive(false);
    }
}
