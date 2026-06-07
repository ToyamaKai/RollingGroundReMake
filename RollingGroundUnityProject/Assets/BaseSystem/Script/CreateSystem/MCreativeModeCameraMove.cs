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
    private Transform   m_transform;
    private float yaw = 0f;
    private float pitch = 0f;
    const   float       k_speed = 1.0f;

    [SerializeField]
    private   float       k_rotationSpeed = 10.0f;

    [SerializeField]
    private float k_moveSpeed = 5.0f;

    [SerializeField]
    private Transform   m_transparentTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_gameInputManager.AddRecieveObject(this);
        m_transform = this.gameObject.transform;
        MMouseCursorManager.Instance.MouseCursorLock();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_cameraDirection != Vector3.zero)
        {
            Vector3 move = transform.right * m_cameraDirection.x + transform.up * m_cameraDirection.y + transform.forward * m_cameraDirection.z;

            m_transform.position += move * k_speed * Time.deltaTime;
        }

        CameraRotation();
    }

    /// <summary>
    /// マウスの移動量に応じてカメラを回転させる処理
    /// </summary>
    private void CameraRotation()
    {
        if(m_gameInputManager.GetActionMapName() == "StageCreative")
        {
            float mouseX = Input.GetAxis("Mouse X") * k_rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * k_rotationSpeed;
            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -89f, 89f);

            m_transparentTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            transform.rotation = Quaternion.Euler(0, yaw, 0f);
        }
        else
        {
            return;
        }
    }

    #region InputAction関連
    public virtual void OnCameraMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 value = context.ReadValue<Vector2>();
            m_cameraDirection = new Vector3(value.x * k_moveSpeed, 0f, value.y * k_moveSpeed);
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
            Debug.Log("MoveUp");
            m_cameraDirection = new Vector3(0f, k_moveSpeed, 0f);
        }
        else if (context.canceled)
        {
            m_cameraDirection = Vector3.zero;
        }
    }

    public virtual void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("MoveDown");
            m_cameraDirection = new Vector3(0f, -k_moveSpeed, 0f);
        }
        else if (context.canceled)
        {
            m_cameraDirection = Vector3.zero;
        }
    }
    #endregion
}
