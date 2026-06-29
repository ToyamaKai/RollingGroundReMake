using NUnit.Framework;
using RollingGround;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreativeModeMenu : IInputReceiver
{
    private MGameInputManager m_gameInputManager;
    private GameObject m_menuGameObject;
    private List<MonoBehaviour> m_subMenus;
    private List<ISubMenu> m_menus = new();
    private ISubMenu m_currentMenu;
    private int m_currentMenuIndex = 0;
    private int m_menuCount = 0;
    private bool m_isMenuOpen;
    private bool m_isSubMenuOpen;

    public CreativeModeMenu(MGameInputManager gameInputManager, List<MonoBehaviour> subMenus)
    {
        m_subMenus = subMenus;
        m_gameInputManager = gameInputManager;
        m_gameInputManager.AddRecieveObject(this);

        foreach (var menu in m_subMenus)
        {
            if (menu is ISubMenu subMenu)
            {
                m_menus.Add(subMenu);
            }
        }

        m_menuCount = m_menus.Count;
    }

    /// <summary>
    /// メニューを開く処理
    /// </summary>
    public void OpenMenu()
    {
        m_menuGameObject.SetActive(true);
        m_gameInputManager.SetActionMap("StageCreativeMenu");
    }

    /// <summary>
    /// メニューを閉じる処理
    /// </summary>
    public void CloseMenu()
    {
        m_menuGameObject.SetActive(false);
        m_gameInputManager.SetActionMap("StageCreative");
    }

    #region 入力処理関連
    public void OnToggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (m_isMenuOpen)
        {
            CloseMenu();
            m_isMenuOpen = false;
        }
        else
        {
            OpenMenu();
            m_isMenuOpen = true;
        }
    }

    /// <summary>
    /// サブメニューの開閉を切り替える処理
    /// </summary>
    /// <param name="context"></param>
    public void OnToggleSubMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (m_isSubMenuOpen)
        {
            m_menus[m_currentMenuIndex].CloseSubMenu();
            m_isSubMenuOpen = false;
        }
        else
        {
            m_menus[m_currentMenuIndex].OpenSubMenu();
            m_isSubMenuOpen = true;
        }
    }

    /// <summary>
    /// 入力に応じてメニュー選択を切り替える
    /// </summary>
    public void OnSelectMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        float direction = context.ReadValue<float>();

        m_currentMenuIndex = ((int)(m_currentMenuIndex + direction) + m_menuCount) % m_menuCount;
    }
#endregion
}