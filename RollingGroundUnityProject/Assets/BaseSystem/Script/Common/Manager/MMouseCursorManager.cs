using UnityEngine;

public class MMouseCursorManager : SingletonMonoBehaviour<MMouseCursorManager>
{
    public void MouseCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MouseCursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
