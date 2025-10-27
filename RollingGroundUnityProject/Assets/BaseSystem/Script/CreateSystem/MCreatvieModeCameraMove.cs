using RollingGround;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// クリエイティブモードにおけるカメラの移動処理
/// </summary>
public class MCreatvieModeCameraMove : MonoBehaviour, IInputReceiver
{
    MGameInputManager m_gameInputManager;
    private Vector3 m_cameraDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_gameInputManager.AddRecieveObject(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 value = context.ReadValue<Vector2>();
            m_cameraDirection = new Vector3(value.x, 0f, value.y);
        }
        else if (context.canceled)
        {

        }
    }
}
