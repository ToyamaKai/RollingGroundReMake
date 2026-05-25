using UnityEngine;
using RollingGround;
using UnityEngine.InputSystem;
using MPLib;

namespace RollingGround
{
    public class PlayerMove : MPObject, IInputReceiver
    {
        MGameInputManager m_gameInputManager;
        MGroundChecker m_groundChecker;
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
        public PlayerMove(MGameInputManager gameInputManager, MGroundChecker groundChecker, GameObject playerObject, Rigidbody playerRigidbody) : base()
        {
            m_gameInputManager = gameInputManager;
            m_groundChecker = groundChecker;
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
            MPlayerState.Instance.SetPlayerMoveState(PlayerMoveState.None);
        }

        public virtual void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 value = context.ReadValue<Vector2>();
                m_playerDirection = new Vector3(value.x, 0, value.y);
            }
            else if (context.canceled)
            {
                m_playerDirection = Vector3.zero;
            }

            //プレイヤーの方向を入力方向に
            if (m_playerDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(m_playerDirection, Vector3.up);
                m_playerObjeeect.transform.rotation = targetRotation;
            }
        }

        public override void FixedTick()
        {
            Vector3 moveVelocity = Vector3.zero;

            // 入力移動
            if (MPlayerState.Instance.GetPlayerMoveState != PlayerMoveState.Rotate)
            {
                if (m_playerDirection != Vector3.zero)
                {
                    moveVelocity = m_playerObjeeect.transform.forward * kSpeed;
                    MPlayerState.Instance.SetPlayerMoveState(PlayerMoveState.Walk);
                }
                else
                {
                    MPlayerState.Instance.SetPlayerMoveState(PlayerMoveState.None);
                }
            }

            // 床移動
            Vector3 groundVelocity = Vector3.zero;

            if (m_groundChecker.CurrentMovingGround != null)
            {
                groundVelocity = m_groundChecker.CurrentMovingGround.DeltaPosition / Time.fixedDeltaTime;
                Debug.Log("Ground Velocity: " + groundVelocity);
            }

            Vector3 finalVelocity = moveVelocity + groundVelocity;
            float currentVerticalVelocity = m_playerRigidbody.linearVelocity.y;
            m_playerRigidbody.linearVelocity = new Vector3(finalVelocity.x, currentVerticalVelocity, finalVelocity.z);

            base.FixedTick();
        }

        public override void Dispose()
        {
            m_gameInputManager?.DeleterecieveObject(this);
            base.Dispose();
        }
    }

}