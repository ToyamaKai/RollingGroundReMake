using UnityEngine;

public class BlockHotbarUI : MonoBehaviour
{
    private BlockHotbar m_blockHotbar;
    private const int k_slotNum = 9;
    private GameObject[] m_slot = new GameObject[k_slotNum];

    [SerializeField]
    private GameObject m_BlockHotbar;

    private void Awake()
    {
        m_blockHotbar = GameObject.FindFirstObjectByType<BlockHotbar>();
        m_blockHotbar.OnSlotChanged += RefleshSlot;
    }

    /// <summary>
    /// 指定したスロットの更新
    /// </summary>
    /// <param name="index"></param>
    private void RefleshSlot(int index)
    {
        //UIの切り替えに修正する
        m_slot[index].SetActive(false);
    }

    /// <summary>
    /// ホットバーの表示切替
    /// </summary>
    private void ToggleDisplayBlockList()
    {
        bool isDisplay;
        isDisplay = m_BlockHotbar.activeSelf ? false : true;
        m_BlockHotbar.SetActive(isDisplay);
    }

    /// <summary>
    /// 破壊時処理
    /// </summary>
    private void OnDestroy()
    {
        m_blockHotbar.OnSlotChanged -= RefleshSlot;
    }
}
