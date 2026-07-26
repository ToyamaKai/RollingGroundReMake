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
        private SubMenuNamePlateController m_subMenuNamePlateController;
        private GameObject m_menuGameObject;

        void Start()
        {
            m_menuGameObject = this.gameObject;
            m_mouseCursorManager            = MMouseCursorManager.Instance;
            m_gameInputManager              = GameObject.FindFirstObjectByType<MGameInputManager>();
            m_creativeModeMenu              = new CreativeModeMenu(m_gameInputManager, m_mouseCursorManager, m_subMenus, m_menuGameObject, m_subMenuNamePlate, m_subMenuNamePlateParent);
            m_subMenuNamePlateController    = new SubMenuNamePlateController(m_subMenuNamePlate, m_subMenuNamePlateParent);

            m_creativeModeMenu.Start();
            m_subMenuNamePlateController.Start(m_creativeModeMenu.GetISubMenuScripts());
        }

        /// <summary>
        /// サブメニューネームプレートリストの開閉処理
        /// </summary>
        /// <param name="setActive"></param>
        public void SwitchSubMenuNamePlateListActive(bool setActive)
        {
            m_subMenuNamePlateController.SwitchSubMenuNamePlateListActive(setActive);
        }

        /// <summary>
        /// 選択されているサブメニューネームプレートを強調表示する処理
        /// </summary>
        /// <param name="preMenuIndex"></param>
        /// <param name="currentMenuIndex"></param>
        public void EmpashizeSubMenuNamePlate(int preMenuIndex, int currentMenuIndex)
        {
            m_subMenuNamePlateController.EmphasizeSubMenuNamePlate(preMenuIndex, currentMenuIndex);
        }
    }
}