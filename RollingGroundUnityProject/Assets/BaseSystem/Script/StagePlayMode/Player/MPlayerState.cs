using System.Collections.Generic;
using UnityEngine;

namespace RollingGround
{
    /// <summary>
    /// ステートのGetter, Setter
    /// </summary>
    public class MPlayerState : SingletonMonoBehaviour<MPlayerState>
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

