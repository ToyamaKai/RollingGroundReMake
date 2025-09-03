using UnityEngine;
using RollingGround;
using UnityEngine.InputSystem;
using MPLib;

namespace RollingGround
{
    public class PlayerMove : MPObject, IInputReceiver
    {
        private PlayerInputAction m_playerInputAction;
        private GameObject m_playerObjeeect;
        private Rigidbody m_playerRigidbody;
        MGameInputManager m_gameInputManager;

        private const float kSpeed = 1f;

        public PlayerMove(MGameInputManager gameInputManager, GameObject playerObject, Rigidbody playerRigidbody) : base()
        {
            m_gameInputManager = gameInputManager;
            m_playerObjeeect = playerObject;
            m_playerRigidbody = playerRigidbody;
        }

        public override void Initialize()
        {
            m_playerInputAction = GameObject.FindFirstObjectByType<PlayerInputAction>();
            base.Initialize();
        }

        public void Start()
        {
            PlayerState.Instance.SetPlayerDirectionState(PlayerDirectionState.Back);
            PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.None);
        }

        public virtual void OnMove(InputAction.CallbackContext context)
        {
            string keyName = context.control.name;

            if (context.started)
            {
                switch (keyName)
                {
                    case "d":
                        if (PlayerState.Instance.GetPlayerDirectionState != PlayerDirectionState.Right)
                        {
                            Vector3 worldAngle = m_playerObjeeect.transform.eulerAngles;
                            worldAngle.y = 90;
                            m_playerObjeeect.transform.eulerAngles = worldAngle;
                            PlayerState.Instance.SetPlayerDirectionState(PlayerDirectionState.Right);
                        }
                        PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.Walk);
                        break;
                    case "a":
                        if (PlayerState.Instance.GetPlayerDirectionState != PlayerDirectionState.Left)
                        {
                            Vector3 worldAngle = m_playerObjeeect.transform.eulerAngles;
                            worldAngle.y = 270;
                            m_playerObjeeect.transform.eulerAngles = worldAngle;
                            PlayerState.Instance.SetPlayerDirectionState(PlayerDirectionState.Left);
                        }
                        PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.Walk);
                        break;
                    case "w":
                        if (PlayerState.Instance.GetPlayerDirectionState != PlayerDirectionState.Back)
                        {
                            Vector3 worldAngle = m_playerObjeeect.transform.eulerAngles;
                            worldAngle.y = 0;
                            m_playerObjeeect.transform.eulerAngles = worldAngle;
                            PlayerState.Instance.SetPlayerDirectionState(PlayerDirectionState.Back);
                        }
                        PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.Walk);
                        break;
                    case "s":
                        if (PlayerState.Instance.GetPlayerDirectionState != PlayerDirectionState.Front)
                        {
                            Vector3 worldAngle = m_playerObjeeect.transform.eulerAngles;
                            worldAngle.y = 180;
                            m_playerObjeeect.transform.eulerAngles = worldAngle;
                            PlayerState.Instance.SetPlayerDirectionState(PlayerDirectionState.Front);
                        }
                        PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.Walk);
                        break;
                }
            }
            else if (context.canceled)
            {
                PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.None);
            }
        }

        public override void Tick()
        {
            Debug.Log("В®Вс");
            if (PlayerState.Instance.GetPlayerMoveState == PlayerMoveState.Walk)
            {
                m_playerRigidbody.AddForce(m_playerObjeeect.transform.forward * kSpeed);
            }
            base.Tick();
        }

        public override void Dispose()
        {
            //m_gameInputManager?.PlayerInput.DeleateRecieveObject();
            base.Dispose();
        }
    }

}