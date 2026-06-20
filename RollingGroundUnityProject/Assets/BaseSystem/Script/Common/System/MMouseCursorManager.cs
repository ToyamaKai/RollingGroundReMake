using UnityEngine;

public class MMouseCursorManager : SingletonMonoBehaviour<MMouseCursorManager>
{
    public void MouseCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MouseUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
