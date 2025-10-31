using RollingGround;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/// <summary>
/// クリエイティブモードにおけるカメラの移動処理
/// </summary>
public class MCreatvieModeCameraMove : MonoBehaviour, IInputReceiver
{
    MGameInputManager   m_gameInputManager;
    private Vector3     m_cameraDirection;
    private Transform     m_transform;
    const   float       k_speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_gameInputManager.AddRecieveObject(this);
        m_transform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_cameraDirection != Vector3.zero)
        {
            m_transform.position += m_cameraDirection * k_speed * Time.deltaTime;
        }
    }

    public virtual void OnCameraMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 value = context.ReadValue<Vector2>();
            m_cameraDirection = new Vector3(value.x, 0f, value.y);
        }
        else if (context.canceled)
        {
            m_cameraDirection = Vector3.zero;
        }
    }
    public virtual void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_transform.position = new Vector3(m_transform.position.x, m_transform.position.y + 1, m_transform.position.z);
        }
    }

    public virtual void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_transform.position = new Vector3(m_transform.position.x, m_transform.position.y - 1, m_transform.position.z);
        }
    }
}
