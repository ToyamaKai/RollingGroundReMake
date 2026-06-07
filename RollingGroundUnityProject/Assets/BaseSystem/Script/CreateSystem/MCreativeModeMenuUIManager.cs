using RollingGround;
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
        private GameObject m_CreativeModeMenuUI;

        private MGameInputManager m_gameInputManager;

        private void Awake()
        {
            m_CreativeModeMenuUI.SetActive(false);
            m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
            m_gameInputManager.AddRecieveObject(this);
        }

        public void OnToggleMenuUI(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            if (m_gameInputManager.GetActionMapName() == "StageCreative")
            {
                MMouseCursorManager.Instance.MouseUnlock();
                m_CreativeModeMenuUI.SetActive(true);
                m_gameInputManager.SetActionMap("StageCreativeMenu");
            }
            else if(m_gameInputManager.GetActionMapName() == "StageCreativeMenu")
            {
                MMouseCursorManager.Instance.MouseCursorLock();
                m_gameInputManager.SetActionMap("StageCreative");
                m_CreativeModeMenuUI.SetActive(false);
            }
        }
    }
}

