using UnityEngine;
using RollingGround;

/// <summary>
/// ƒvƒŒƒCƒ„[‚ÌˆÚ“®ˆ—
/// </summary>
public class MPlayerMove : MonoBehaviour
{
    PlayerMove m_playerMove;

    MGameInputManager m_gameInputManager;

    [SerializeField]
    GameObject m_playerGameObject;

    [SerializeField]
    Rigidbody m_playerRigidBbody;

    private void Awake()
    {
        m_playerMove = new PlayerMove();
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_playerMove.Initialize(m_gameInputManager, m_playerGameObject, m_playerRigidBbody);
    }

    public void Update()
    {
        m_playerMove.Tick();
    }
}
