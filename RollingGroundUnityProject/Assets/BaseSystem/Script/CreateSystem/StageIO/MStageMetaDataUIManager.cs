using UnityEngine;
using UnityEngine.UI;

public class MStageMetaDataUIManager : MonoBehaviour
{
    private StageMetaDataFactory m_factory = new StageMetaDataFactory();
    private string m_stageName;
    private string m_comment;

    [SerializeField]
    private InputField m_stageNameInputFiel;

    [SerializeField]
    private InputField m_commentInputField;

    public void SetStageName()
    {

    }
}
