using NUnit.Framework;
using RollingGround;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreativeModeMenu : MonoBehaviour, IInputReceiver
{
    [SerializeField]
    private List<MonoBehaviour> m_subMenus;

    private MGameInputManager m_gameInputManager;

    private List<ISubMenu> m_menus = new();

    private ISubMenu m_currentMenu;

    private int m_currentMenuIndex = 0;

    private int m_menuCount = 0;

    private void Awake()
    {
        foreach(var menu in m_subMenus)
        {
            if(menu is ISubMenu subMenu)
            {
                m_menus.Add(subMenu);
            }
        }

        m_menuCount = m_menus.Count;
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
}
