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

            if(m_playerInput == null)
            {
                Debug.Log("見つからないよぉ～");
            }
            else
            {
                Debug.Log("見つかったねぇ～");
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnMove(context);
            }
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            foreach(var recieveObject in m_inputReceieveObjectList)
            {
                recieveObject.OnRotate(context);
            }
        }

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
    }
}
