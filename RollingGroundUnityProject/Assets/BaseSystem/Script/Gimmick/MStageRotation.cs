using UnityEngine;
using UnityEngine.InputSystem;
using RollingGround.Logic;
using DG.Tweening;

namespace RollingGround
{
    /// <summary>
    /// キー入力によるステージの回転処理やアニメーションを行う
    /// </summary>
    public class MStageRotation : MonoBehaviour, IInputReceiver
    {
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
            Debug.Log($"{m_currentData.StepX}, {m_currentData.StepY}, {m_currentData.StepZ}");

            //ワールド座標での回転を行うように修正する
            Vector3 targetEuler = new Vector3(
                m_currentData.StepX * 90f,
                m_currentData.StepY * 90f,
                m_currentData.StepZ * 90f
                );

            m_stageRoot.DORotate(targetEuler, m_rotateDuration).SetEase(Ease.OutQuint).OnComplete(() => m_isRotating = false);
        }
    }
}
