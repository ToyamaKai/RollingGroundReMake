using NUnit.Framework;
using RollingGround;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RollingGround
{
    public class MGameInputManager : MonoBehaviour
    {
        private PlayerInput m_playerInput;
        public PlayerInput PlayerInput => m_playerInput;

        private readonly List<IInputReceiver> m_inputReceieveObjectList = new();

        private void Awake()
        {
            m_playerInput = GetComponent<PlayerInput>();
            m_playerInput.SwitchCurrentActionMap("StageCreative");
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnMove(context);
            }
        }

        public void OnStageRotate(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnStageRotate(context);
            }
        }

        #region ステージクリエイティブモード
        public void OnBlockSet(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnBlockSet(context);
            }
        }

        public void OnDeleteBlock(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnDeleteBlock(context);
            }
        }

        public void OnBlockHeightChange(InputAction.CallbackContext context)
        {
            foreach (var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnBlockHeightChange(context);
            }
        }

        public void OnMoveUp(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnMoveUp(context);
            }
        }

        public void OnMoveDown(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnMoveDown(context);
            }
        }

        public void OnCameraMove(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnCameraMove(context);
            }
        }

        public void OnCameraRotation(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnCameraRotation(context);
            }
        }

        public void OnSelectSlotChange(InputAction.CallbackContext context)
        {
            foreach(var recievesObject in m_inputReceieveObjectList)
            {
                recievesObject.OnSelectSlotChange(context);
            }
        }
        #endregion


        #region オブジェクトの追加と削除
        /// <summary>
        /// 入力を受け取るオブジェクトの追加
        /// </summary>
        /// <param name="inputRecieverObject"></param>
        public void AddRecieveObject(IInputReceiver inputRecieverObject)
        {
            m_inputReceieveObjectList.Add(inputRecieverObject);
        }

        /// <summary>
        /// 入力を受け取るオブジェクトの削除
        /// </summary>
        /// <param name="inputRecieverObject"></param>
        public void DeleterecieveObject(IInputReceiver inputRecieverObject)
        {
            m_inputReceieveObjectList.Remove(inputRecieverObject);
        }

        /// <summary>
        /// 入力を受け取るオブジェクトのクリア
        /// </summary>
        public void ClearRecieveObject()
        {
            m_inputReceieveObjectList.Clear();
        }
        #endregion
    }
}
