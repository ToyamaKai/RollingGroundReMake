using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// サブメニューネームプレートリストに関する処理
/// </summary>
public class SubMenuNamePlateController
{
    private GameObject m_subMenuNamePlatePrefab;
    private GameObject m_subMenuNamePlateParent;
    private List<GameObject> m_subMenuNamePlateList = new List<GameObject>();


    // サブメニューの表示位置用定数
    private const float k_subMenuPositionX = -660.0f;
    private const float k_subMenuEmphasizePositionX = -600.0f;
    private const float k_subMenuPositionYOrigin = 440.0f;
    private const float k_subMenuPositionYInterval = 200.0f;

    public SubMenuNamePlateController(GameObject subMenuNamePlatePrefab, GameObject subMenuNamePlateParent)
    {
        m_subMenuNamePlatePrefab = subMenuNamePlatePrefab;
        m_subMenuNamePlateParent = subMenuNamePlateParent;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="ISubMenuScripts"></param>
    public void Start(List<ISubMenu> ISubMenuScripts)
    {
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

        for(int i = 0; i <  m_subMenuNamePlateList.Count; i++)
        {
            // 先頭のネームプレートのX座標をずらし選択状態とするための三項演算子
            float positionX = i == 0 ? k_subMenuEmphasizePositionX : k_subMenuPositionX;
            subMenuNamePlate = Object.Instantiate(m_subMenuNamePlatePrefab, m_subMenuNamePlateParent.transform, false);
            subMenuNamePlate.transform.localPosition = new Vector3(positionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * i), 0);

            subMenuNamePlateText = subMenuNamePlate.GetComponentInChildren<Text>();
            subMenuNamePlateText.text = menus[i].GetSubMenuName();

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
    /// <param name="preMenuIndex">現在選択されているネームプレート番号</param>
    /// <param name="currentMenuIndex">次に選択されるネームプレート番号</param>
    public void EmphasizeSubMenuNamePlate(int preMenuIndex, int currentMenuIndex)
    {
        m_subMenuNamePlateList[preMenuIndex].transform.localPosition = new Vector3(k_subMenuPositionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * preMenuIndex), 0);
        m_subMenuNamePlateList[currentMenuIndex].transform.localPosition = new Vector3(k_subMenuEmphasizePositionX, k_subMenuPositionYOrigin - (k_subMenuPositionYInterval * currentMenuIndex), 0);
        preMenuIndex = currentMenuIndex;
    }
}
