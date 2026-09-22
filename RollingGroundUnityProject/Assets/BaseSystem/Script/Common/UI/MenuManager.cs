using RollingGround;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メニュー画面の制御処理
/// </summary>
public class MenuManager : IInputReceiver
{
    private ISubMenu m_returnMenu = null; // 閉じる際にリターン先のサブメニューを指定するための変数
    private Dictionary<MenuType, ISubMenu> m_menus = new Dictionary<MenuType, ISubMenu>();

    public void Initialize(List<ISubMenu> subMenuScript)
    {
        RegisterSubMenus(subMenuScript);
    }

    /// <summary>
    /// DictionaryにISubMenuを登録する処理
    /// </summary>
    public void RegisterSubMenus(List<ISubMenu> subMenus)
    {
        foreach (var menu in subMenus)
        {
            m_menus.Add(menu.Type, menu);
        }
    }

    #region サブメニュー開閉に関する処理
    /// <summary>
    /// サブメニューを開く処理
    /// </summary>
    /// <param name="subMenuScript"></param>
    public void OpenSubMenu(MenuType menuType)
    {
        m_menus[menuType].OpenSubMenu();
    }

    /// <summary>
    /// サブメニューを閉じる処理
    /// </summary>
    /// <param name="subMenuScript"></param>
    public void CloseSubMenu(MenuType menuType)
    {
        m_menus[menuType].CloseSubMenu();
    }

    /// <summary>
    /// 閉じる際にリターン先のサブメニューを指定する処理
    /// </summary>
    /// <param name="subMenuScript"></param>
    /// <param name="returnMenu"></param>
    public void CloseSubMenuWithReturn(MenuType toMenu, MenuType returnMenu)
    {
        m_menus[toMenu].OpenSubMenu();
        m_returnMenu = m_menus[returnMenu];
    }

    /// <summary>
    /// リターン先のサブメニューを開く処理
    /// </summary>
    public void ReturnToPreviousSubMenu()
    {
        if (m_returnMenu != null)
        {
            m_returnMenu.OpenSubMenu();
            m_returnMenu = null;
        }
    }
    #endregion
}