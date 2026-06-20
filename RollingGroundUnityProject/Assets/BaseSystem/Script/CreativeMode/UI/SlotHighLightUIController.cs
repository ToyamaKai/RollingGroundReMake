using System;
using UnityEngine;

public class SlotHighLightUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_highLightSlot;
    private BlockHotbar m_blockHotbar;
    private const float k_slotSpace = 160.0f;
    private const float k_firstSlotPositionX = -640.0f;

    private void Awake()
    {
        m_blockHotbar = GameObject.FindFirstObjectByType<BlockHotbar>();
        m_blockHotbar.OnSelectedSlotChanged += MoveHightLightUI;
        ResetHightLightUI();
    }

    private void ResetHightLightUI()
    {
        Vector3 UIposition = m_highLightSlot.transform.position;
        UIposition = new Vector3(k_firstSlotPositionX, 0, 0);
        m_highLightSlot.transform.localPosition = UIposition;
    }

    private void MoveHightLightUI(int slotNum)
    {
        float position = k_firstSlotPositionX + (k_slotSpace * slotNum);
        Vector3 UIposition = m_highLightSlot.transform.localPosition;
        UIposition.x = position;

        m_highLightSlot.transform.localPosition = UIposition;
    }
}
