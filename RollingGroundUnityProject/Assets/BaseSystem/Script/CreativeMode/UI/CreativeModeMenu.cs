using NUnit.Framework;
using RollingGround;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreativeModeMenu : IInputReceiver
{
    private MGameInputManager m_gameInputManager;
    private MMouseCursorManager m_mouseCursorManager;
    private GameObject m_menuGameObject;
    private List<GameObject> m_subMenus;
    private List<ISubMenu> m_menus = new();
    private ISubMenu m_currentMenu;
    private int m_currentMenuIndex = 0;
    private int m_menuCount = 0;
    private bool m_isMenuOpen;
    private bool m_isSubMenuOpen;

    public CreativeModeMenu(MGameInputManager gameInputManager, MMouseCursorManager mouseCursorManager, List<GameObject> subMenus, GameObject menuGameObject)
    {
        m_subMenus = subMenus;
        m_gameInputManager = gameInputManager;
        m_mouseCursorManager = mouseCursorManager;
        m_menuGameObject = menuGameObject;
        m_gameInputManager.AddRecieveObject(this);

        foreach (var menu in m_subMenus)
        {
            var iSubmenu = menu.GetComponent<ISubMenu>();

            if(iSubmenu != null)
            {
                m_menus.Add(iSubmenu);
                iSubmenu.CloseSubMenu();
            }
            else
            {
                Debug.LogWarning($"GameObject {menu.name} does not implement ISubMenu.");
            }
        }

        m_menuCount = m_menus.Count;
        CloseMenu();
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
    /// <summary>
    /// メニューの開閉を切り替える処理
    /// </summary>
    /// <param name="context"></param>
    public void OnToggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (m_isMenuOpen)
        {
            m_mouseCursorManager.MouseCursorLock();

            if (m_isSubMenuOpen)
            {
                m_currentMenu.CloseSubMenu();
                m_isSubMenuOpen = false;
            }

            CloseMenu();
            m_isMenuOpen = false;
        }
        else
        {
            OpenMenu();
            m_isMenuOpen = true;
            m_mouseCursorManager.MouseCursorUnlock();
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
            m_currentMenu.CloseSubMenu();
            m_isSubMenuOpen = false;
        }
        else
        {
            m_currentMenu = m_menus[m_currentMenuIndex];
            m_menus[m_currentMenuIndex].OpenSubMenu();
            m_isSubMenuOpen = true;
        }
    }

    /// <summary>
    /// 入力に応じてメニュー選択を切り替える
    /// </summary>
    public void OnSelectMenu(InputAction.CallbackContext context)
    {
        if (!context.performed || !m_isMenuOpen || m_isSubMenuOpen) return;
        float direction = context.ReadValue<float>();

        m_currentMenuIndex = ((int)(m_currentMenuIndex + direction) + m_menuCount) % m_menuCount;
    }
#endregion
}