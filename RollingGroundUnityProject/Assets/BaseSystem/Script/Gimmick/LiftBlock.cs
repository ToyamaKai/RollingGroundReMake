using DG.Tweening;
using UnityEngine;

/// <summary>
/// リフトブロック
/// </summary>
public class LiftBlock
{
    private Vector3 m_moveDirection = new Vector3(0, 0, 0);   //移動方向
    private float m_moveDistance = 0f;  //移動距離
    private float m_moveSpeed = 0f; //移動速度
    private float m_moveSec; //移動時間
    private float m_waitTime; //待機時間
    private LiftTriggerType m_liftTriggerType = LiftTriggerType.Always; //リフトのトリガータイプ
    private Transform m_transform; //リフトブロックのTransform
    private Vector3 m_originPosition; //最初のポジション
    private Vector3 m_position; //移動先のポジション
    private Vector3 m_prePosition; //1フレーム前のポジション
    private Sequence m_liftSequence;

    public LiftBlock(Vector3 moveDirection, float moveDistance, float moveSpeed, float waitTime, LiftTriggerType liftTriggerType, Transform transform) : base()
    {
        m_moveDirection = moveDirection;
        m_moveDistance = moveDistance;
        m_moveSpeed = moveSpeed;
        m_waitTime = waitTime;
        m_liftTriggerType = liftTriggerType;
        m_transform = transform;
        m_originPosition = transform.localPosition;
        m_position = m_originPosition + m_moveDirection.normalized * m_moveDistance;
        m_moveSec = m_moveDistance / m_moveSpeed;
        m_prePosition = m_originPosition;
    }

    /// <summary>
    /// オブジェクトが指定した秒数で指定した位置に移動するアニメーション
    /// </summary>
    public void Move()
    {
        m_liftSequence?.Kill();
        m_liftSequence = DOTween.Sequence();
        m_liftSequence.AppendInterval(m_waitTime);

        m_liftSequence.Append(
            m_transform.DOLocalMove(m_position, m_moveSec)
            .SetEase(Ease.InOutSine)
        );

        m_liftSequence.AppendInterval(m_waitTime);

        m_liftSequence.Append(
            m_transform
            .DOLocalMove(m_originPosition, m_moveSec)
            .SetEase(Ease.InOutSine)
        );

        m_liftSequence.SetLoops(-1);
    }

    /// <summary>
    /// 前フレームからの移動量を計算する関数
    /// 必ずLateUpdateで扱うこと
    /// </summary>
    /// <returns></returns>
    public Vector3 DeltaPosition()
    {
        var deltaPosition = m_transform.localPosition - m_prePosition;
        m_prePosition = m_transform.localPosition;
        return deltaPosition;
    }
}
