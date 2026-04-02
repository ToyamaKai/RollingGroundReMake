using UnityEngine;
using UnityEngine.InputSystem;
using RollingGround.Logic;
using DG.Tweening;
using System;

namespace RollingGround
{
    /// <summary>
    /// キー入力によるステージの回転処理やアニメーションを行う
    /// </summary>
    public class MStageRotation : MonoBehaviour, IInputReceiver
    {
        public static event Action<Transform> OnStageRotateStart; // ステージ回転開始イベント
        public static event Action<Transform> OnStageRotateUpdate; // ステージ回転更新イベント
        public static event Action OnStageRotateComplete; // ステージ回転完了イベント

        [SerializeField]
        private Transform m_stageRoot;

        [SerializeField]
        private float m_rotateDuration = 0.5f;

        private StageData m_currentData = new StageData();
        private bool m_isRotating = false;  //回転中かの変数

        void Awake()
        {
            var gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            gameInputManager.AddRecieveObject(this);
        }

        /// <summary>
        /// ステージ回転キーを押下した際の挙動
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnStageRotate(InputAction.CallbackContext context)
        {
            // 1. performed 以外のフェーズ（Canceledなど）を弾く
            if (!context.performed) return;

            // 2. 回転中なら絶対に受け付けない（連打防止の再確認）
            if (m_isRotating) return;

            float value = context.ReadValue<float>();
            string actionName = context.action.name;

            if (actionName == "StageRotateX") RotateStage(RotationAxis.X, (int)value);
            if (actionName == "StageRotateY") RotateStage(RotationAxis.Y, (int)value);
            if (actionName == "StageRotateZ") RotateStage(RotationAxis.Z, (int)value);
        }

        /// <summary>
        /// ステージオブジェクトの回転アニメーション
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="dir"></param>
        private void RotateStage(RotationAxis axis, int dir)
        {
            m_isRotating = true;

            StageRotationLogic.ExecuteRotate(m_currentData, axis, dir);

            // 2. 演出：今の角度に「世界の軸」で90度足すだけ
            Vector3 rotationAmount = axis switch
            {
                RotationAxis.X => Vector3.right * (90 * dir),
                RotationAxis.Y => Vector3.up * (90 * dir),
                RotationAxis.Z => Vector3.forward * (90 * dir),
                _ => Vector3.zero
            };

            //回転開始を通知
            OnStageRotateStart?.Invoke(m_stageRoot);

            // DORotate(加算量, 時間, モード) の一番短い書き方
            m_stageRoot.DORotate(rotationAmount, m_rotateDuration, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutQuint)
                .SetUpdate(UpdateType.Fixed)
                .OnUpdate(() =>
                {
                    OnStageRotateUpdate?.Invoke(m_stageRoot);
                })
                .OnComplete(() =>
                {
                    m_isRotating = false;
                    OnStageRotateComplete?.Invoke();
                });
        }
    }
}
