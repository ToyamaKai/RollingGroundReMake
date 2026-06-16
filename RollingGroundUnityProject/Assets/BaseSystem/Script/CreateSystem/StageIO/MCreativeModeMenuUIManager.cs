using Newtonsoft.Json;
using RollingGround;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// クリエイティブモードにおけるメニュー画面の管理スクリプト
/// </summary>
namespace RollingGround
{
    public class MCreativeModeMenuUIManager : MonoBehaviour, IInputReceiver
    {
        [SerializeField]
        private MStageDataIOButtonHandler m_stageDataIOButtonHandler;

        [SerializeField]
        private MStageMetadataInputHandler m_stageMetaDataInputHandler;

        private MGameInputManager m_gameInputManager;
        private bool m_isToggling = false;

        void Start()
        {
            m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            m_gameInputManager.AddRecieveObject(this);
            SetStageDataIOUIActive(false);
            SetStageMetaDataUIActive(false);
        }

        /// <summary>
        /// メタデータUIの表示切替
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="stageDataExport"></param>
        public void SetStageMetaDataUIActive(bool isActive, Action stageDataExport = null)
        {
            m_stageMetaDataInputHandler.SetMetaDataInputUIActive(isActive, stageDataExport);
        }

        /// <summary>
        /// ステージデータIOUIの表示切替
        /// </summary>
        /// <param name="isActive"></param>
        public void SetStageDataIOUIActive(bool isActive)
        {
            m_stageDataIOButtonHandler.SetStageDataIOUIActive(isActive);
        }

        #region キー入力に対する処理関連
        /// <summary>
        /// キー入力に対すMenuUIの表示切替及び、ActionMapsの切り替え
        /// </summary>
        /// <param name="context"></param>
        public void OnToggleMenuUI(InputAction.CallbackContext context)
        {
            if (!context.performed || m_isToggling) return;

            if (m_gameInputManager.GetActionMapName() == "StageCreative")
            {
                MMouseCursorManager.Instance.MouseUnlock();
                StartCoroutine(ToggleMenuUI(false));
            }
            else if(m_gameInputManager.GetActionMapName() == "StageCreativeMenu")
            {
                MMouseCursorManager.Instance.MouseCursorLock();
                StartCoroutine(ToggleMenuUI(true));
            }
        }

        /// <summary>
        /// MenuUIの表示状態を切り替え
        /// </summary>
        /// <returns></returns>
        public IEnumerator ToggleMenuUI(bool isActive)
        {
            m_isToggling = true;
            m_stageDataIOButtonHandler.SetStageDataIOUIActive(!isActive);

            if(!isActive)
            {
                m_gameInputManager.SetActionMap("StageCreativeMenu");
            }
            else
            {
                m_gameInputManager.SetActionMap("StageCreative");
            }

            yield return null;
            m_isToggling = false;
        }
        #endregion
    }
}

