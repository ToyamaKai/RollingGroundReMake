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
    private Vector2     m_lookInput;
    private float       m_sensitivity = 100;
    private float       m_cameraXRotation = 0;
    const   float       k_speed = 1.0f;

    [SerializeField]
    private Transform   m_transparentTransform;


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
        //CameraRotation();
    }

    //private void CameraRotation()
    //{
    //    float mouseX = m_lookInput.x * m_sensitivity * Time.deltaTime;
    //    float mouseY = m_lookInput.y * m_sensitivity * Time.deltaTime;
    //    m_cameraXRotation -= mouseY;
    //    m_cameraXRotation = Mathf.Clamp(m_cameraXRotation, -40f, 40f);
    //    m_transform.localRotation = Quaternion.Euler(m_cameraXRotation, 0f, 0f);

    //    m_transparentTransform.Rotate(Vector3.up * mouseX);
    //}

    #region InputAction関連
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

    public virtual void OnCameraRotation(InputAction.CallbackContext context)
    {
        m_lookInput = context.ReadValue<Vector2>();
    }
    #endregion
}
