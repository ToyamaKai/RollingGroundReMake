using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CreativeModeMenu : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> m_subMenus;

    private List<ISubMenu> m_menus = new();

    private ISubMenu m_currentMenu;

    private int m_currentMenuIndex = 0;

    private void Awake()
    {
        foreach(var menu in m_subMenus)
        {
            if(menu is ISubMenu subMenu)
            {
                m_menus.Add(subMenu);
            }
        }
    }

    /// <summary>
    /// 入力に応じてメニュー選択を切り替える(1の増減)
    /// </summary>
    public void OnMoveMenu()
    {

    }
}
