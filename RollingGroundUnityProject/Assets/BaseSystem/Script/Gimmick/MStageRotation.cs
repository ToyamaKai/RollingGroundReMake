using Cysharp.Threading.Tasks;
using DG.Tweening;
using RollingGround;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

//ステージ回転処理
public class MStageRotation : IInputReceiver
{
    MGameInputManager m_gameInputManager;
    GameObject m_stageRoot;

    void Awake()
    {
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_gameInputManager.AddRecieveObject(this);
    }

    public virtual void OnStageRotate(InputAction.CallbackContext context)
    {
        InputControl control = context.control;
    }
}
