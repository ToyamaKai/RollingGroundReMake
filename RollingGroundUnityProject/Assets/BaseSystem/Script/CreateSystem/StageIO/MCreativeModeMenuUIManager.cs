using Newtonsoft.Json;
using RollingGround;
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
        private GameObject m_creativeModeMenuUI;

        [SerializeField]
        private GameObject m_stageMetaDataUI;

        private MStageManager m_stageManager;
        private JSONConverter m_JSONConverter;
        private MGameInputManager m_gameInputManager;
        private MStageMetaDataUIManager m_stageMetaDataUIManager;
        private bool m_isToggling = false;

        void Start()
        {
            m_creativeModeMenuUI.SetActive(false);
            m_stageManager = MStageManager.Instance;
            m_JSONConverter = GameObject.FindFirstObjectByType<JSONConverter>();
            m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            m_stageMetaDataUIManager = GameObject.FindFirstObjectByType<MStageMetaDataUIManager>();
            m_gameInputManager.AddRecieveObject(this);
        }

        #region メニュー画面におけるボタンの処理
        public void OnStageExportButton()
        {
            if(!m_stageManager.GetIsSaved())
            {
                StageMetaDataUISetActive(true);
            }
            else
            {
                m_JSONConverter.StageJSONConvert();
            }
        }

        public void OnStageImportButton(string dataPath)
        {
            m_JSONConverter.StageJsonDeserialize(dataPath);
        }

        #endregion

        public void StageMetaDataUISetActive(bool isActive)
        {
            m_stageMetaDataUI.SetActive(isActive);
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
            m_creativeModeMenuUI.SetActive(!isActive);

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

