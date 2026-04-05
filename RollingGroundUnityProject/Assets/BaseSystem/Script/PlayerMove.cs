using UnityEngine;
using RollingGround;
using UnityEngine.InputSystem;
using MPLib;

namespace RollingGround
{
    public class PlayerMove : MPObject, IInputReceiver
    {
        MGameInputManager m_gameInputManager;
        private GameObject m_playerObjeeect;
        private Rigidbody m_playerRigidbody;
        private Vector3 m_playerDirection;
        private const float kSpeed = 1f;

        /// <summary>
        /// 必要な要素などを受け取る
        /// </summary>
        /// <param name="gameInputManager"></param>
        /// <param name="playerObject"></param>
        /// <param name="playerRigidbody"></param>
        public PlayerMove(MGameInputManager gameInputManager, GameObject playerObject, Rigidbody playerRigidbody) : base()
        {
            m_gameInputManager = gameInputManager;
            m_playerObjeeect = playerObject;
            m_playerRigidbody = playerRigidbody;
            m_gameInputManager.AddRecieveObject(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Start()
        {
            PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.None);
        }

        public virtual void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 value = context.ReadValue<Vector2>();
                m_playerDirection = new Vector3(value.x, 0f, value.y);
                PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.Walk);
            }
            else if (context.canceled)
            {
                PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.None);
            }

            //プレイヤーの方向を入力方向に
            if (m_playerDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(m_playerDirection, Vector3.up);
                m_playerObjeeect.transform.rotation = targetRotation;
            }
        }

        public override void Tick()
        {

            //プレイヤーをforward方向にAddforceで移動させる
            if (PlayerState.Instance.GetPlayerMoveState == PlayerMoveState.Walk)
            {
                m_playerRigidbody.linearVelocity = m_playerObjeeect.transform.forward * kSpeed;
            }

            base.Tick();
        }

        public override void Dispose()
        {
            m_gameInputManager?.DeleterecieveObject(this);
            base.Dispose();
        }
    }

}