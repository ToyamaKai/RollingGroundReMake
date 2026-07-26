using NUnit.Framework;
using RollingGround;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CreativeModeMenu : IInputReceiver
{
    private MGameInputManager   m_gameInputManager;         // ゲーム入力マネージャー
    private MMouseCursorManager m_mouseCursorManager;       // マウスカーソルマネージャー
    private GameObject          m_menuGameObject;           // メニューUI
    private GameObject          m_subMenuNamePlate;         // サブメニューのネームプレートプレハブ(Instantiate用)
    private GameObject          m_subMenuNamePlateParent;   // サブメニューのネームプレートの親オブジェクト(開閉、親設定用)
    private List<GameObject>    m_subMenus;                 // サブメニューUIのリスト
    private List<GameObject>    m_subMenuNamePlateList = new List<GameObject>();     // サブメニューのネームプレートリスト
    private List<ISubMenu>      m_menus = new();            // ISubMenuインターフェースを継承しているスクリプトリスト
    private ISubMenu            m_currentMenu;              // 現在選択されているサブメニューのISubMenuインターフェースを継承しているスクリプト
    private int                 m_currentMenuIndex = 0;     // 現在選択されているサブメニューの番号
    private int                 m_preMenuIndex = 0;         // ひとつ前に選択されたサブメニューの番号
    private int                 m_menuCount = 0;            // サブメニューの数
    private bool                m_isMenuOpen;               // メニューの開閉状況
    private bool                m_isSubMenuOpen;            // サブメニューの開閉状況

    // サブメニューの表示位置用定数
    private const float k_subMenuPositionX          = -660.0f;
    private const float k_subMenuEmphasizePositionX = -600.0f;
    private const float k_subMenuPositionYOrigin    = 440.0f;
    private const float k_subMenuPositionYInterval  = 200.0f;

    public CreativeModeMenu(MGameInputManager gameInputManager, MMouseCursorManager mouseCursorManager, List<GameObject> subMenus, GameObject menuGameObject, GameObject subMenuNamePlate, GameObject subMenuNamePlateParent)
    {
        m_subMenus = subMenus;
        m_gameInputManager = gameInputManager;
        m_mouseCursorManager = mouseCursorManager;
        m_menuGameObject = menuGameObject;
        m_gameInputManager.AddRecieveObject(this);
        m_subMenuNamePlate = subMenuNamePlate;
        m_subMenuNamePlateParent = subMenuNamePlateParent;
    }

    public void Start()
    {
        // サブメニューからISubMenuを継承するスクリプトを取得しリストに格納
        foreach (var menu in m_subMenus)
        {
            var iSubmenu = menu.GetComponent<ISubMenu>();

            if (iSubmenu != null)
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
        SubMenuNamePlateInstantiate();
        CloseMenu();
    }

    /// <summary>
    /// ISubMenuを継承したスクリプトを返すメソッド
    /// </summary>
    /// <returns></returns>
    public List<ISubMenu> GetISubMenuScripts()
    {
        if( m_menus == null )
        {
            Debug.Log("ISubMenuリストはNull");
            return null;
        } else
        {
            return m_menus;
        }
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

    #region サブメニューネームプレート関連の処理

    /// <summary>
    /// サブメニューネームプレートの生成処理
    /// </summary>
    public void SubMenuNamePlateInstantiate()
    {
        GameObject subMenuNamePlate;
        Text subMenuName;
        for(int i = 0; i < m_subMenus.Count; i++)
        {
            float positionX = i == 0? k_subMenuEmphasizePositionX : k_subMenuPositionX;
            subMenuNamePlate = Object.Instantiate(m_subMenuNamePlate, m_subMenuNamePlateParent.transform, false);
            subMenuNamePlate.transform.localPosition = new Vector3(positionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * i), 0);

            subMenuName = subMenuNamePlate.GetComponentInChildren<Text>();
            subMenuName.text = m_menus[i].GetSubMenuName();

            m_subMenuNamePlateList.Add(subMenuNamePlate);
        }
    }

    /// <summary>
    /// サブメニューネームプレートリストの開閉処理
    /// </summary>
    /// <param name="setActive"></param>
    public void SwitchSubMenuNamePlateListActive(bool setActive)
    {
        m_subMenuNamePlateParent.SetActive(setActive);
    }
    
    /// <summary>
    /// 選択されているサブメニューネームプレートを強調表示する処理
    /// </summary>
    public void EmphasizeSubMenuNamePlate()
    {
        m_subMenuNamePlateList[m_preMenuIndex].transform.localPosition = new Vector3(k_subMenuPositionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * m_preMenuIndex), 0);
        m_subMenuNamePlateList[m_currentMenuIndex].transform.localPosition = new Vector3(k_subMenuEmphasizePositionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * m_currentMenuIndex), 0);
        m_preMenuIndex = m_currentMenuIndex;
    }

    #endregion

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
            SwitchSubMenuNamePlateListActive(false);
            CloseMenu();
            m_isMenuOpen = false;
        }
        else
        {
            SwitchSubMenuNamePlateListActive(true);
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
            SwitchSubMenuNamePlateListActive(true);
            m_isSubMenuOpen = false;
        }
        else
        {
            m_currentMenu = m_menus[m_currentMenuIndex];
            SwitchSubMenuNamePlateListActive(false);
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

        EmphasizeSubMenuNamePlate();
    }
#endregion
}