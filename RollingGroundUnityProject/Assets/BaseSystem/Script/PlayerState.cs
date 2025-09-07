using System.Collections.Generic;
using UnityEngine;

namespace RollingGround
{
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

        private PlayerMoveState m_moveState;

        #region Getter

        public PlayerMoveState GetPlayerMoveState => m_moveState;
        #endregion

        #region Setter

        public void SetPlayerMoveState(PlayerMoveState moveState) => m_moveState = moveState;
        #endregion
    }
}

