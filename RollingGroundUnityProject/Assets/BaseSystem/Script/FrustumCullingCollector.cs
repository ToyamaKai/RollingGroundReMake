using System.Collections.Generic;
using MPLib;
using NUnit.Framework;
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

        //消すかも
        private GameObject hoge;
        private List<Renderer> m_touchedObjectRenderer = new List<Renderer>();

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
            Vector3 normal = (m_mainCamera.transform.position - m_playerObject.transform.position).normalized;
            Plane farPlane = new Plane(normal, m_playerObject.transform.position);
            FrustumPlaneList[5] = farPlane;
            return FrustumPlaneList;
        }

        //Todo視錐台に接するブロックの取得
        //public List<GameObject> GetTouchedObjectList()
        //{
        //}

        public override void Tick()
        {
            m_touchedObjectRenderer.Clear();

            Renderer[] renderers = hoge.GetComponentsInChildren<Renderer>();
            Plane[] planes = UpdateFrustumToPlayerDistance();

            foreach (Renderer renderer in renderers)
            {
                if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
                {
                    m_touchedObjectRenderer.Add(renderer);
                }
            }

            for (int i = 0; i < m_touchedObjectRenderer.Count; i++)
            {
                Debug.Log(m_touchedObjectRenderer[i]);
            }


            Debug.Log("hoge");

            base.Tick();
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}