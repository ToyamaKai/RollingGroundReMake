using UnityEngine;
using RollingGround;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Round<TInput, TOutput>;

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
            switch(keyName)
            {
                case "D":
                    if (!isRight)
                    {
                        worldAngle = this.transform.eulerAngles;
                        worldAngle.y += 180;
                        this.transform.eulerAngles = worldAngle;
                        isRight = true;
                    }
                    Runing = true;
                    rb.AddForce(transform.forward * speed);
                    break;
            }
        }
    }
}
