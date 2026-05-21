using UnityEngine;

public class MLiftBlock : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField]
    private Vector3 m_moveDirection = Vector3.up; //移動方向

    [SerializeField]
    private float m_moveDistance = 3.0f; //移動距離

    [SerializeField]
    private float m_moveSpeed = 2.0f; //移動速度

    [Header("トリガー設定")]
    [SerializeField]
    private LiftTriggerType m_liftTriggerType = LiftTriggerType.Always; //リフトのトリガータイプ

    private LiftBlock m_liftBlock;

    private void Awake()
    {
        m_liftBlock = new LiftBlock(m_moveDirection, m_moveDistance, m_moveSpeed, m_liftTriggerType);
    }

    //TODO: LiftBlockに記述したリフトブロックの動く処理を呼び出す処理の記述
}
