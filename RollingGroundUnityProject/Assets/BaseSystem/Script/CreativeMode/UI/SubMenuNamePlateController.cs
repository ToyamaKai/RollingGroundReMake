using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// サブメニューのネームプレート描画に関する処理
/// </summary>
public class SubMenuNamePlateController
{
    private List<GameObject>    m_menuNamePlateList  = new List<GameObject>();        // サブメニューのネームプレートのリスト
    private List<MenuType>      m_menuTypeList = new List<MenuType>();             // サブメニューの種類のリスト
    private GameObject          m_subMenuNamePlatePrefab;   // サブメニューネームプレートの複製用Prefab
    private GameObject          m_subMenuNamePlateParent;   // サブメニューネームプレートの親オブジェクト

    // サブメニューのネームプレートの表示を制御するための変数
    private int m_currentMenuIndex  = 0;    // 現在選択されているサブメニューの番号
    private int m_preMenuIndex      = 0;    // ひとつ前に選択されたサブメニューの番号
    private int m_menuCount         = 0;    // サブメニューの数

    // サブメニューの表示位置用定数
    private const float k_subMenuPositionX          = -660.0f;
    private const float k_subMenuEmphasizePositionX = -600.0f;
    private const float k_subMenuPositionYOrigin    = 440.0f;
    private const float k_subMenuPositionYInterval  = 200.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="subMenuNamePlatePrefab">サブメニューネームプレートの複製用Prefab</param>
    /// <param name="subMenuNamePlateParent">サブメニューネームプレートの親オブジェクト</param>
    /// <param name="ISubMenuScripts">サブメニューのスクリプトリスト</param>
    public void Initialize(GameObject subMenuNamePlatePrefab, GameObject subMenuNamePlateParent, List<ISubMenu> ISubMenuScripts)
    {
        m_subMenuNamePlatePrefab = subMenuNamePlatePrefab;
        m_subMenuNamePlateParent = subMenuNamePlateParent;
        m_menuCount = ISubMenuScripts.Count;
        SubMenuNamePlateInstantiate(ISubMenuScripts);
    }

    /// <summary>
    /// サブメニューネームプレートの生成処理
    /// </summary>
    /// <param name="menus"></param>
    public void SubMenuNamePlateInstantiate(List<ISubMenu> menus)
    {
        GameObject subMenuNamePlate;
        Text subMenuNamePlateText;

        for(int i = 0; i < m_menuCount; i++)
        {
            // 先頭のネームプレートのX座標をずらし選択状態とするための三項演算子
            float positionX = i == 0 ? k_subMenuEmphasizePositionX : k_subMenuPositionX;
            subMenuNamePlate = Object.Instantiate(m_subMenuNamePlatePrefab, m_subMenuNamePlateParent.transform, false);
            subMenuNamePlate.transform.localPosition = new Vector3(positionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * i), 0);

            subMenuNamePlateText = subMenuNamePlate.GetComponentInChildren<Text>();
            subMenuNamePlateText.text = menus[i].GetSubMenuName();

            m_menuNamePlateList.Add(subMenuNamePlate);
            m_menuTypeList.Add(menus[i].Type);
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
    /// 現在選択中のメニュータイプを返す
    /// </summary>
    /// <returns></returns>
    public MenuType GetNowMenu()
    {
        return m_menuTypeList[m_currentMenuIndex];
    }

    /// <summary>
    /// 選択中のサブメニューを強調表示する処理
    /// </summary>
    /// <param name="direction"></param>
    public void SelectMenuEmphasize(int direction)
    {
        m_currentMenuIndex = ((m_currentMenuIndex + direction) + m_menuCount) % m_menuCount;

        var normalNamePlatePosition     = new Vector3(k_subMenuPositionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * m_preMenuIndex), 0);
        var emphasizeNamePlatePosition  = new Vector3(k_subMenuEmphasizePositionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * m_currentMenuIndex), 0);
        m_menuNamePlateList[m_preMenuIndex].transform.localPosition = normalNamePlatePosition;
        m_menuNamePlateList[m_currentMenuIndex].transform.localPosition = emphasizeNamePlatePosition;

        m_preMenuIndex = m_currentMenuIndex;
    }
}
