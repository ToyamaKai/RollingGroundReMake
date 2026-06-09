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

        private MGameInputManager m_gameInputManager;
        private bool m_isToggling = false;

        void Start()
        {
            m_creativeModeMenuUI.SetActive(false);
            m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            m_gameInputManager.AddRecieveObject(this);
        }

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
    }
}

