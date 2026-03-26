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
            //TODO: 入力されたキーをもとに処理層に渡す記述をしてくれ...
            InputControl control = context.control;
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

            Vector3 targetEuler = new Vector3(
                m_currentData.StepX * 90f,
                m_currentData.StepY * 90f,
                m_currentData.StepZ * 90f
                );

            m_stageRoot.DORotate(targetEuler, m_rotateDuration).SetEase(Ease.OutQuint).OnComplete(() => m_isRotating = false);
        }
    }
}
