using UnityEngine;

/// <summary>
/// リフトブロック
/// </summary>
public class LiftBlock
{
    private Vector3 m_moveDirection = new Vector3(0, 0, 0);   //移動方向
    private float m_moveDistance = 0f;           //移動距離
    private float m_moveSpeed = 0f;              //移動速度
    private LiftTriggerType m_liftTriggerType = LiftTriggerType.Always; //リフトのトリガータイプ

    public LiftBlock(Vector3 moveDirection, float moveDistance, float moveSpeed, LiftTriggerType liftTriggerType) : base()
    {
        m_moveDirection = moveDirection;
        m_moveDistance = moveDistance;
        m_moveSpeed = moveSpeed;
        m_liftTriggerType = liftTriggerType;
    }

    //TODO: DOTWeen等を用いたリフトブロックの動く処理の記述
}
