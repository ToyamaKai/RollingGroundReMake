using UnityEngine;
using RollingGround;
using MPLib;

namespace RollingGround
{
    public class MFrustum : MMPObject
    {
        [SerializeField]
        Camera m_mainCamera;

        [SerializeField]
        GameObject m_player;

        [SerializeField]
        GameObject ParentObj;

        private FrustumCullingCollector m_frustumCullector;

        protected override void ConstructSelf()
        {
            m_frustumCullector = new FrustumCullingCollector(m_mainCamera, m_player, ParentObj);
            Injection(m_frustumCullector);
            base.ConstructSelf();
        }
    }
}
