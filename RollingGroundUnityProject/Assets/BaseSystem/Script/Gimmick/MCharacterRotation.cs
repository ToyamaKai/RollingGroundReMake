using UnityEngine;
using UnityEngine.InputSystem;
using RollingGround.Logic;
using DG.Tweening;

namespace RollingGround
{
    /// <summary>
    /// ステージの回転に併せてキャラクターの回転を行うクラス
    /// </summary>
    public class MCharacterRotation : MonoBehaviour, IInputReceiver
    {
        [SerializeField]
        private Transform m_characterWorldRoot; //ワールド原点取得用

        [SerializeField]
        private Transform m_characterRoot; //キャラ原点取得用

        [SerializeField]
        private float m_rotateDuration = 0.5f;

        private PlayerData m_currentData = new PlayerData();
        private bool m_isRotating = false;  //回転中かの変数

        void Awake()
        {
            var gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            gameInputManager.AddRecieveObject(this);
        }

        public virtual void OnStageRotate(InputAction.CallbackContext context)
        {
            if (!context.performed || m_isRotating) return;

            float value = context.ReadValue<float>();
            string actionName = context.action.name;

            if (actionName == "StageRotateX") RotateStage(CharacterRotationAxis.X, (int)value);
            if (actionName == "StageRotateY") RotateStage(CharacterRotationAxis.Y, (int)value);
            if (actionName == "StageRotateZ") RotateStage(CharacterRotationAxis.Z, (int)value);
        }

        /// <summary>
        /// ステージオブジェクトの回転アニメーション
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="dir"></param>
        private void RotateStage(CharacterRotationAxis axis, int dir)
        {
            m_isRotating = true;
            PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.Rotate); 

            CharacterRotationLogic.ExecutePlayerRotate(m_currentData, axis, dir);

            Vector3 rotationAmount = axis switch
            {
                CharacterRotationAxis.X => Vector3.right * (90 * dir),
                CharacterRotationAxis.Y => Vector3.up * (90 * dir),
                CharacterRotationAxis.Z => Vector3.forward * (90 * dir),
                _ => Vector3.zero
            };

            //ワールド原点を中心に回転(位置追従用)
            m_characterWorldRoot.DOBlendableRotateBy(rotationAmount, m_rotateDuration, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => m_isRotating = false);

            //キャラ原点を中心に反対に回転(向き調整用)
            m_characterRoot.DOBlendableRotateBy(-rotationAmount, m_rotateDuration, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => PlayerState.Instance.SetPlayerMoveState(PlayerMoveState.None));
        }
    }
}
