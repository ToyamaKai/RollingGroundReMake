using UnityEngine;
using RollingGround;

/// <summary>
/// ƒvƒŒƒCƒ„[‚ÌˆÚ“®ˆ—
/// </summary>
public class MPlayerMove : MonoBehaviour
{
    PlayerMove m_playerMove;
    MGameInputManager m_gameInputManager;

    private void Awake()
    {
        m_playerMove = new PlayerMove();
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_playerMove.Initialize(m_gameInputManager);
    }
}
