using RollingGround;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// 状態ステート(別に書き出すかも)
/// </summary>
public enum MenuState
{
    OnGame,
    Menu,
    SubMenu,
}

/// <summary>
/// クリエイティブモードにおけるメニュー画面の制御クラス
/// </summary>
public class CreativeModeMenuManager : IInputReceiver
{
    private MGameInputManager   m_gameInputManager;         // ゲーム入力マネージャー
    private MMouseCursorManager m_mouseCursorManager;       // マウスカーソルマネージャー
    private MenuState m_menuState;

    private MenuManager m_menuManager;
    private SubMenuNamePlateController m_subMenuNamePlateController;

    private bool m_hasReturnMenu = false;
    private MenuType m_menuType;
    private MenuType m_returnMenuType;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="gameInputManager">ゲーム入力処理制御スクリプト</param>
    /// <param name="mouseCursorManager">マウスカーソル制御スクリプト</param>
    /// <param name="creativeModeMenuUIManager"></param>
    /// <param name="subMenus"></param>
    /// <param name="menuGameObject"></param>
    /// <param name="subMenuNamePlateParent"></param>
    public void Initialize(MGameInputManager gameInputManager, MMouseCursorManager mouseCursorManager, List<ISubMenu> subMenus, GameObject menuGameObject, GameObject subMenuNamePlateParent)
    {
        m_menuState                     = MenuState.OnGame;
        m_gameInputManager              = gameInputManager;
        m_mouseCursorManager            = mouseCursorManager;
        m_menuManager                   = new MenuManager();
        m_subMenuNamePlateController    = new SubMenuNamePlateController();

        m_gameInputManager.AddRecieveObject(this);
        m_menuManager.Initialize(subMenus);
        m_subMenuNamePlateController.Initialize(menuGameObject, subMenuNamePlateParent, subMenus);
    }

    /// <summary>
    /// 閉じる際にリターン先のサブメニューを指定する処理
    /// </summary>
    /// <param name="toMenu"></param>
    /// <param name="returnMenu"></param>
    public void CloseSubMenuWithReturn(MenuType toMenu, MenuType returnMenu)
    {
        m_hasReturnMenu = true;
        m_menuType = toMenu;
        m_returnMenuType = returnMenu;
        m_menuManager.CloseSubMenuWithReturn(toMenu, returnMenu);
    }

    /// <summary>
    /// 元のメニュー画面に戻る処理
    /// </summary>
    public void ReturnToPreviousSubMenu()
    {
        m_menuManager.ReturnToPreviousSubMenu();
        m_menuType = m_returnMenuType;
    }

    /// <summary>
    /// メニューを閉じる際の処理
    /// </summary>
    public void CloseMenu()
    {
        if(m_hasReturnMenu)
        {
            m_hasReturnMenu = false;
        }

        m_menuState = MenuState.OnGame;
        m_gameInputManager.SetActionMap("StageCreative");
        m_mouseCursorManager.MouseCursorUnlock();
    }

    /// <summary>
    /// サブメニューを閉じる際の処理
    /// </summary>
    public void CloseSubMenu()
    {
        if (m_hasReturnMenu)
        {
            ReturnToPreviousSubMenu();
            m_hasReturnMenu = false;
        }
        else
        {
            m_subMenuNamePlateController.SwitchSubMenuNamePlateListActive(true);
            m_menuState = MenuState.Menu;
            m_mouseCursorManager.MouseCursorLock();
        }
    }

    #region 入力処理関連
    /// <summary>
    /// メニューの開閉を切り替える処理
    /// </summary>
    /// <param name="context"></param>
    public void OnToggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;


        if (m_menuState == MenuState.OnGame)
        {
            m_menuState = MenuState.Menu;
            m_subMenuNamePlateController.SwitchSubMenuNamePlateListActive(true);
            m_gameInputManager.SetActionMap("StageCreativeMenu");
            m_mouseCursorManager.MouseCursorLock();
        }
        else if (m_menuState == MenuState.Menu)
        {
            m_subMenuNamePlateController.SwitchSubMenuNamePlateListActive(false);
            CloseMenu();
        }
        else if (m_menuState == MenuState.SubMenu)
        {
            m_menuManager.CloseSubMenu(m_menuType);
            CloseMenu();
        }
    }

    /// <summary>
    /// サブメニューの開閉を切り替える処理
    /// </summary>
    /// <param name="context"></param>
    public void OnToggleSubMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        EventSystem.current.SetSelectedGameObject(null);

        if (m_menuState == MenuState.Menu)
        {
            m_menuType = m_subMenuNamePlateController.GetNowMenu();
            m_subMenuNamePlateController.SwitchSubMenuNamePlateListActive(false);
            m_menuState = MenuState.SubMenu;
            m_menuManager.OpenSubMenu(m_menuType);
            m_mouseCursorManager.MouseCursorUnlock();
        }
        else if(m_menuState == MenuState.SubMenu)
        {
            m_menuManager.CloseSubMenu(m_menuType);
            CloseSubMenu();
        }
    }

    /// <summary>
    /// 入力に応じてメニュー選択を切り替える
    /// </summary>
    public void OnSelectMenu(InputAction.CallbackContext context)
    {
        if (!context.performed || m_menuState != MenuState.Menu) return;
        float direction = context.ReadValue<float>();

        m_subMenuNamePlateController.SelectMenuEmphasize((int) direction);
    }
#endregion
}