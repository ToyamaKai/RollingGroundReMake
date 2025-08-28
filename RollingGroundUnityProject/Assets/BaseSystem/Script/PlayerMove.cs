using UnityEngine;
using RollingGround;
using UnityEngine.InputSystem;

public class PlayerMove : IInputReceiver
{
    private PlayerInputAction m_playerInputAction;
    MGameInputManager m_gameInputManager;

    public void Initialize(MGameInputManager gameInputManager)
    {
        m_gameInputManager = gameInputManager;
        gameInputManager.AddRecieveObject(this);
        m_playerInputAction = GameObject.FindFirstObjectByType<PlayerInputAction>();
    }

    public virtual void OnMove(InputAction.CallbackContext context)
    {
        string keyName = context.control.name;

        if(context.performed)
        {
            Debug.Log("ìÆÇ¢ÇƒÇÈÇÊÅ`");
        }
    }
}
