using UnityEngine;
using RollingGround;
using UnityEngine.InputSystem;

public class PlayerMove : IInputReceiver
{
    private PlayerInputAction m_playerInputAction;
    private GameObject m_playerObjeeect;
    private Rigidbody m_playerRigidbody;
    MGameInputManager m_gameInputManager;

    private const float kSpeed = 0.5f;

    public void Initialize(MGameInputManager gameInputManager, GameObject playerObject, Rigidbody playerRigidbody)
    {
        m_gameInputManager = gameInputManager;
        m_playerObjeeect = playerObject;
        m_playerRigidbody = playerRigidbody;
        gameInputManager.AddRecieveObject(this);
        m_playerInputAction = GameObject.FindFirstObjectByType<PlayerInputAction>();

        PlayerState.Instance.SetPlayerDirectionState(PlayerDirectionState.Back);
    }

    public virtual void OnMove(InputAction.CallbackContext context)
    {
        string keyName = context.control.name;

        if(context.performed)
        {
            switch(keyName)
            {
                case "d":
                    if (PlayerState.Instance.GetPlayerDirectionState != PlayerDirectionState.Right)
                    {
                        Vector3 worldAngle = m_playerObjeeect.transform.eulerAngles;
                        worldAngle.y = -270;
                        m_playerObjeeect.transform.eulerAngles = worldAngle;
                        PlayerState.Instance.SetPlayerDirectionState(PlayerDirectionState.Right);
                    }
                    m_playerRigidbody.AddForce(m_playerObjeeect.transform.forward * kSpeed);
                    break;
            }
        }
    }
}
