using System;
using UnityEngine;

/// <summary>
/// リフトのプロパティデータクラス
/// </summary>
[Serializable]
public class LiftPropertyData
{
    public Vector3 Direction;
    public float Distance;
    public float Speed;
    public float WaitTime;
    public LiftTriggerType TriggerType;
}