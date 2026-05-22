using Cysharp.Threading.Tasks;
using DG.Tweening;
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
    private Transform m_transform; //リフトブロックのTransform

    public LiftBlock(Vector3 moveDirection, float moveDistance, float moveSpeed, LiftTriggerType liftTriggerType, Transform transform) : base()
    {
        m_moveDirection = moveDirection;
        m_moveDistance = moveDistance;
        m_moveSpeed = moveSpeed;
        m_liftTriggerType = liftTriggerType;
        m_transform = transform;
    }

    //TODO: DOTWeen等を用いたリフトブロックの動く処理の記述
    public async UniTask Move()
    {
        await m_transform.DOLocalMove(m_transform.position + m_moveDirection.normalized * m_moveDistance, m_moveDistance / m_moveSpeed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine).AsyncWaitForCompletion();
    }
}
