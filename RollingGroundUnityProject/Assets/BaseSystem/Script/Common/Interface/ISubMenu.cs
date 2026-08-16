using System;
using UnityEngine;

public interface ISubMenu
{
    void OpenSubMenu();
    void CloseSubMenu();
    string GetSubMenuName();
}
