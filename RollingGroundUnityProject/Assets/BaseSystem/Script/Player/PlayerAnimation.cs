using UnityEngine;
using MPLib;

namespace RollingGround
{
    /// <summary>
    /// プレイヤーのアニメーションを管理するクラス
    /// </summary>
    public class PlayerAnimation : MPObject
    {
        private Animator m_animator;

        public PlayerAnimation(Animator animator) : base()
        {
            m_animator = animator;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Tick()
        {
            var state = (int)MPlayerState.Instance.GetPlayerMoveState;
            m_animator.SetInteger("PlayerMoveState", state);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
