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

            Vector3 normal = (m_mainCamera.transform.position - m_playerObject.transform.position).normalized;
            Plane farPlane = new Plane(normal, m_playerObject.transform.position);

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
            VisualizeFarPlane(planes[5]);

            //Debug用の可視化（Sceneビュー / Gameビュー両方に出る）
            VisualizeFrustum(planes);

            foreach (Renderer renderer in renderers)
            {
                if (renderer == null || renderer.gameObject == null) continue;

                if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
                {
                    m_touchedObjectRenderer.Add(renderer);
                }
            }

            m_blockOutline.UpdateOutline(m_touchedObjectRenderer);

            foreach (var item in m_touchedObjectRenderer)
            {
                Debug.Log(item);
            }

            Debug.Log("hoge");

            base.Tick();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Debug.Draw系で視錐台とFarPlaneを可視化
        /// </summary>
        private void VisualizeFrustum(Plane[] frustumPlanes)
        {
            if (frustumPlanes == null || m_mainCamera == null) return;

            // カメラの位置・方向を基準にFrustumのコーナーを計算
            Vector3[] corners = new Vector3[8];
            m_mainCamera.CalculateFrustumCorners(
                new Rect(0, 0, 1, 1),
                m_mainCamera.farClipPlane,
                Camera.MonoOrStereoscopicEye.Mono,
                corners
            );

            // 近クリップ面のコーナー
            Vector3[] nearCorners = new Vector3[4];
            m_mainCamera.CalculateFrustumCorners(
                new Rect(0, 0, 1, 1),
                m_mainCamera.nearClipPlane,
                Camera.MonoOrStereoscopicEye.Mono,
                nearCorners
            );

            Transform camTf = m_mainCamera.transform;

            // 近面と遠面の座標に変換
            Vector3[] worldFar = new Vector3[4];
            Vector3[] worldNear = new Vector3[4];
            for (int i = 0; i < 4; i++)
            {
                worldFar[i] = camTf.TransformPoint(corners[i]);
                worldNear[i] = camTf.TransformPoint(nearCorners[i]);
            }

            // 線を描画（青色）
            for (int i = 0; i < 4; i++)
            {
                Debug.DrawLine(worldNear[i], worldNear[(i + 1) % 4], Color.blue); // near
                Debug.DrawLine(worldFar[i], worldFar[(i + 1) % 4], Color.blue);   // far
                Debug.DrawLine(worldNear[i], worldFar[i], Color.blue);            // side
            }

            // FarPlaneを赤い法線で表示
            var farPlane = frustumPlanes[5];
            Vector3 planePoint = m_playerObject.transform.position;
            Debug.DrawRay(planePoint, farPlane.normal * 5f, Color.red);
        }

        // farPlaneそのものを赤い四角で描く例
        private void VisualizeFarPlane(Plane farPlane)
        {
            // 平面を表す代表点（プレイヤー位置を通るのでそれを利用）
            Vector3 center = m_playerObject.transform.position;

            // farPlaneの法線から、平面上の2軸を作る
            Vector3 normal = farPlane.normal;
            Vector3 tangent = Vector3.Cross(normal, Vector3.up).normalized;
            if (tangent == Vector3.zero)
                tangent = Vector3.Cross(normal, Vector3.right).normalized;
            Vector3 bitangent = Vector3.Cross(normal, tangent).normalized;

            float size = 3f; // 平面の描画サイズ
            Vector3 p1 = center + (tangent + bitangent) * size;
            Vector3 p2 = center + (tangent - bitangent) * size;
            Vector3 p3 = center + (-tangent - bitangent) * size;
            Vector3 p4 = center + (-tangent + bitangent) * size;

            Debug.DrawLine(p1, p2, Color.red);
            Debug.DrawLine(p2, p3, Color.red);
            Debug.DrawLine(p3, p4, Color.red);
            Debug.DrawLine(p4, p1, Color.red);

            // 法線を矢印で
            Debug.DrawRay(center, normal * 2f, Color.yellow);
        }

    }
}
