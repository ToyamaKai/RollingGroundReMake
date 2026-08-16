using RollingGround;
using UnityEngine;

public class MSubMenuNamePlateController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_subMenuNamePlatePrefab;

    [SerializeField]
    private MCreativeModeMenuUIManager m_creativeModeMenuUIManager;

    private SubMenuNamePlateController m_subMenuNamePlateController;
    private GameObject m_subMenuNamePlateListObject;

    private void Awake()
    {
        m_subMenuNamePlateListObject = this.gameObject;
    }

    private void Start()
    {
        m_subMenuNamePlateController = new SubMenuNamePlateController(m_subMenuNamePlatePrefab, m_subMenuNamePlateListObject, m_creativeModeMenuUIManager);
    }
}
