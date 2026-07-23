using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリエイティブモードにおけるメニュー画面の管理スクリプト
/// </summary>
namespace RollingGround
{
    public class MCreativeModeMenuUIManager : MonoBehaviour, IInputReceiver
    {
        [SerializeField]
        private List<GameObject> m_subMenus;

        [SerializeField]
        private GameObject m_subMenuNamePlate;

        [SerializeField]
        private GameObject m_subMenuNamePlateParent;

        private MGameInputManager m_gameInputManager;
        private MMouseCursorManager m_mouseCursorManager;
        private CreativeModeMenu m_creativeModeMenu;
        private GameObject m_menuGameObject;

        void Start()
        {
            m_menuGameObject = this.gameObject;
            m_mouseCursorManager = MMouseCursorManager.Instance;
            m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            m_creativeModeMenu = new CreativeModeMenu(m_gameInputManager, m_mouseCursorManager, m_subMenus, m_menuGameObject, m_subMenuNamePlate, m_subMenuNamePlateParent);
        }
    }
}