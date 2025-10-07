using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// カメラがプレイヤーを追いかけるスクリプト
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Transform m_PlayerTransform;

    private const float m_cameraDistance = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = m_PlayerTransform.position - new Vector3(0, -1, m_cameraDistance);
        this.gameObject.transform.position = cameraPosition;
    }
}
