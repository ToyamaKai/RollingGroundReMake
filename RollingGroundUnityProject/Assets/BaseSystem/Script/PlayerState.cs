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

    /// <summary>
    /// ステートのGetter, Setter
    /// </summary>
    public class PlayerState : SingletonMonoBehaviour<PlayerState>
    {
        private PlayerDirectionState m_playerDirectionState;

        #region Getter
        public PlayerDirectionState GetPlayerDirectionState => m_playerDirectionState;
        #endregion

        #region Setter
        public void SetPlayerDirectionState(PlayerDirectionState Direction) => m_playerDirectionState = Direction;
        #endregion

    }
}

