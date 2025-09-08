using NUnit.Framework;
using UnityEngine;

/// <summary>
/// 視錐台に内接・接するブロックを取得しリスト化するアルゴリズム
/// </summary>
public class FrustumCullingCollector
{
    private Camera      m_mainCamera;
    private GameObject  m_playerObject;

    private List hoge;

    public FrustumCullingCollector(Camera mainCamera, GameObject playerObject)
    {
        m_mainCamera    = mainCamera;
        m_playerObject  = playerObject;
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

}
