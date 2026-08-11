using System;
using UnityEngine;

public interface ISubMenu
{
    void OpenSubMenu(Action onClose);
    void CloseSubMenu();
    string GetSubMenuName();
}
