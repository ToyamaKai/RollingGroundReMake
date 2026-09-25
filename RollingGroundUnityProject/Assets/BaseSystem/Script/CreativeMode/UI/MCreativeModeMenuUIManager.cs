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
        private GameObject m_subMenuNamePlate;

        [SerializeField]
        private GameObject m_subMenuNamePlateParent;

        private MGameInputManager m_gameInputManager;
        private MMouseCursorManager m_mouseCursorManager;
        private CreativeModeMenuManager m_creativeModeMenu;

        private List<ISubMenu> m_subMenus = new List<ISubMenu>();

        void Awake()
        {
            m_subMenus = new List<ISubMenu>(GetComponentsInChildren<ISubMenu>(true)){};
        }

        void Start()
        {
            m_mouseCursorManager    = MMouseCursorManager.Instance;
            m_gameInputManager      = GameObject.FindFirstObjectByType<MGameInputManager>();
            m_creativeModeMenu      = new CreativeModeMenuManager();
            m_creativeModeMenu.Initialize(m_gameInputManager, m_mouseCursorManager, m_subMenus, m_subMenuNamePlate, m_subMenuNamePlateParent);
        }

        public void CloseSubMenuWithReturn(MenuType toMenu, MenuType returnMenu)
        {
            m_creativeModeMenu.CloseSubMenuWithReturn(toMenu, returnMenu);
        }

        public void ReturnToPreviousSubMenu()
        {
            m_creativeModeMenu.ReturnToPreviousSubMenu();
        }

        public void CloseMenu()
        {
            m_creativeModeMenu.CloseSubMenu();
        }
    }
}