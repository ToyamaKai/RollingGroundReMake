    using UnityEngine;

public class MLiftBlock : MonoBehaviour, IMovingGround
{
    [Header("移動設定")]
    [SerializeField]
    private Vector3 m_moveDirection = Vector3.up; //移動方向

    [SerializeField]
    private float m_moveDistance = 3.0f; //移動距離

    [SerializeField]
    private float m_moveSpeed = 2.0f; //移動速度

    [SerializeField]
    private float m_waitTime = 1.0f; //待機時間

    [Header("トリガー設定")]
    [SerializeField]
    private LiftTriggerType m_liftTriggerType = LiftTriggerType.Always; //リフトのトリガータイプ

    private LiftBlock m_liftBlock;
    public Vector3 DeltaPosition { get; private set; }

    void Awake()
    {
        m_liftBlock = new LiftBlock(m_moveDirection, m_moveDistance, m_moveSpeed, m_waitTime, m_liftTriggerType, this.transform);
        m_liftBlock.Move();
    }

    private void FixedUpdate()
    {
        DeltaPosition = m_liftBlock.DeltaPosition();
    }

    //TODO: LiftBlockに記述したリフトブロックの動く処理を呼び出す処理の記述
}
