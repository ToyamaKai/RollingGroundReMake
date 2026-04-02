using UnityEngine;
using RollingGround.Logic;
using SN = System.Numerics;

namespace RollingGround
{
    /// <summary>
    /// ステージの回転に併せてキャラクターの回転を行うクラス
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MCharacterRotation : MonoBehaviour
    {
        private Rigidbody m_rb;
        private SN.Vector3 m_localOffset; // キャラクターのローカルオフセット
        private bool m_wasUseGravity;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
            m_rb.interpolation = RigidbodyInterpolation.Interpolate; // 滑らかな動きを実現するために補間を有効化
        }

        private void OnEnable()
        {
            MStageRotation.OnStageRotateStart += HandleRotateStart;
            MStageRotation.OnStageRotateUpdate += HandleRotateUpdate;
            MStageRotation.OnStageRotateComplete += HandleRotateComplete;
        }

        private void OnDisable()
        {
            MStageRotation.OnStageRotateStart -= HandleRotateStart;
            MStageRotation.OnStageRotateUpdate -= HandleRotateUpdate;
            MStageRotation.OnStageRotateComplete -= HandleRotateComplete;
        }

        /// <summary>
        /// 回転開始の通知を受け取った際にキャラクターのローカルオフセットを保存する
        /// </summary>
        private void HandleRotateStart(Transform stageRoot)
        {
            m_rb.isKinematic = true;

            m_localOffset = StageRotationLogic.CalculateInitialOffset(
                m_rb.position.ToSystem(),
                stageRoot.position.ToSystem(),
                stageRoot.rotation.ToSystem()
            );
        }

        private void HandleRotateUpdate(Transform stageRoot)
        {
            //m_rb.linearVelocity = Vector3.zero;

            // ロジックで計算した結果を .ToUnity() で戻して適用
            var newPos = StageRotationLogic.CalculateFollowPosition(
                m_localOffset,
                stageRoot.position.ToSystem(),
                stageRoot.rotation.ToSystem()
            );

            m_rb.position = newPos.ToUnity();
        }

        private void HandleRotateComplete()
        {
            m_rb.linearVelocity = Vector3.zero;
            m_rb.angularVelocity = Vector3.zero;
            m_rb.isKinematic = false;
        }
    }
}
