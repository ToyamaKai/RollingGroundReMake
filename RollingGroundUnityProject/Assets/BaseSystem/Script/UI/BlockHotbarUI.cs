using UnityEngine;

public class BlockHotbarUI : MonoBehaviour
{
    private BlockHotbar m_blockHotbar;

    private void Awake()
    {
        m_blockHotbar = GameObject.FindFirstObjectByType<BlockHotbar>();
        m_blockHotbar.OnSlotChanged += RefleshSlot;
    }

    private void RefleshSlot(int index)
    {

    }

    private void DisplayBlockList()
    {

    }

    /// <summary>
    /// îjâÛéûèàóù
    /// </summary>
    private void OnDestroy()
    {
        m_blockHotbar.OnSlotChanged -= RefleshSlot;
    }
}
