using UnityEngine;

/// <summary>
/// 地面判定用クラス
/// </summary>
public class MGroundChecker : MonoBehaviour
{
    [SerializeField]
    private float m_checkDistance = 0.5f; //地面判定の距離

    public bool IsGrounded { get; private set; }

    public IMovingGround CurrentMovingGround { get; private set; }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, m_checkDistance);

        if(IsGrounded)
        {
            hit.collider.TryGetComponent(out IMovingGround movingGround);
            CurrentMovingGround = hit.collider.GetComponent<IMovingGround>();
        }
        else
        {
            CurrentMovingGround = null;
        }
    }
}
