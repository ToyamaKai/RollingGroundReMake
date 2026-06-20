using UnityEngine;
using MPLib;

namespace RollingGround
{
    public class MPlayerAnimation : MMPObject
    {
        PlayerAnimation m_playerAnimation;

        [SerializeField]
        Animator m_animator;

        protected override void ConstructSelf()
        {
            m_playerAnimation = new PlayerAnimation(m_animator);
            Injection(m_playerAnimation);
            m_playerAnimation.Initialize();
            base.ConstructSelf();
        }
    }
}
