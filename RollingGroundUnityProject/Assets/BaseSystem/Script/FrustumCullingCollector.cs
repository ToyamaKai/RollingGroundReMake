using System.Collections.Generic;
using MPLib;
using UnityEngine;

/// <summary>
/// 視錐台に内接・接するブロックを取得しリスト化するアルゴリズム
/// </summary>
namespace RollingGround
{
    public class FrustumCullingCollector : MPObject
    {
        private Camera m_mainCamera;
        private GameObject m_playerObject;

        private BlockOutline m_blockOutline;

        //消すかも
        private GameObject hoge;
        private HashSet<Renderer> m_touchedObjectRenderer = new HashSet<Renderer>();

        public FrustumCullingCollector(Camera mainCamera, GameObject playerObject, GameObject Hoge) : base()
        {
            m_mainCamera = mainCamera;
            m_playerObject = playerObject;
            hoge = Hoge;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// 視錐台のPlaneリスト内のFarPlane面をプレイヤーからカメラ方面の法線で作成したプレイヤー位置のPlaneに置き換える
        /// </summary>
        public Plane[] UpdateFrustumToPlayerDistance()
        {
            var FrustumPlaneList = GeometryUtility.CalculateFrustumPlanes(m_mainCamera);

            Vector3 planePoint = new Vector3(m_mainCamera.transform.position.x, m_mainCamera.transform.position.y, Mathf.Floor(m_playerObject.transform.position.z) - 1 );
            Plane farPlane = new Plane(-m_mainCamera.transform.forward,  planePoint);

            // FarPlaneを差し替え
            FrustumPlaneList[5] = farPlane;

            return FrustumPlaneList;
        }

        public override void Tick()
        {
            m_touchedObjectRenderer.Clear();

            if (!m_blockOutline)
                m_blockOutline = GameObject.FindFirstObjectByType<BlockOutline>();

            Renderer[] renderers = hoge.GetComponentsInChildren<Renderer>();
            Plane[] planes = UpdateFrustumToPlayerDistance();

            foreach (Renderer renderer in renderers)
            {
                if (renderer == null || renderer.gameObject == null) continue;

                if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
                {
                    m_touchedObjectRenderer.Add(renderer);
                }
            }

            m_blockOutline.UpdateOutline(m_touchedObjectRenderer);

            base.Tick();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
