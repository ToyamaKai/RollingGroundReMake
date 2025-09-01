using UnityEngine;

namespace RollingGround
{
    public enum PlayerDirectionState
    {
        Right,
        Left,
        Front,
        Back,
    }

    public enum PlayerMoveState
    {
        None,
        Walk,
        Run,
        Jump,
    }

    /// <summary>
    /// ステートのGetter, Setter
    /// </summary>
    public class PlayerState : SingletonMonoBehaviour<PlayerState>
    {
        private PlayerDirectionState m_playerDirectionState;

        private PlayerMoveState m_moveState;

        #region Getter
        public PlayerDirectionState GetPlayerDirectionState => m_playerDirectionState;

        public PlayerMoveState GetPlayerMoveState => m_moveState;
        #endregion

        #region Setter
        public void SetPlayerDirectionState(PlayerDirectionState direction) => m_playerDirectionState = direction;

        public void SetPlayerMoveState(PlayerMoveState moveState) => m_moveState = moveState;
        #endregion
    }
}

