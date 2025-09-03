using UnityEngine;
using RollingGround;
using MPLib;

/// <summary>
/// ƒvƒŒƒCƒ„[‚ÌˆÚ“®ˆ—
/// </summary>
namespace RollingGround
{
    public class MPlayerMove : MMPObject
    {
        PlayerMove m_playerMove;

        MGameInputManager m_gameInputManager;

        [SerializeField]
        GameObject m_playerGameObject;

        [SerializeField]
        Rigidbody m_playerRigidBbody;

        protected override void ConstructSelf()
        {
            m_playerMove = new PlayerMove(m_gameInputManager, m_playerGameObject, m_playerRigidBbody);
            m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            Injection(m_playerMove);
            m_playerMove.Initialize();
            base.ConstructSelf();
        }

        protected override void TerminateSelf()
        {
            //m_playerMove.Dispose();
            base.TerminateSelf();
        }

        private void Start()
        {
            m_playerMove.Start();
        }

        public PlayerMove GetPlayerMove() => m_playerMove;
    }

}