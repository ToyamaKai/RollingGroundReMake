using System;
using UnityEngine;

/// <summary>
/// クリエイティブモードにおけるホットバーを管理するクラス
/// </summary>
public class BlockHotbar : SingletonMonoBehaviour<BlockHotbar>
{
    private const int k_maxHotbarSize = 9; //ホットバーの最大サイズ

    [SerializeField]
    private BlockData[] m_hotbar = new BlockData[k_maxHotbarSize]; //ホットバー
    private int m_selectedIndex;    //選択中のスロット番号
    public event Action<int> OnSlotChanged; //スロットの変更イベント

    private void Awake()
    {
        m_selectedIndex = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(m_selectedIndex != 8)
            {
                m_selectedIndex++;
            }
            else
            {
                m_selectedIndex = 0;
            }
        }
    }

    /// <summary>
    /// ホットバー(クローン)を全て返すメソッド
    /// </summary>
    /// <returns></returns>
    public ScriptableObject[] GetHotbar()
    {
        return (ScriptableObject[])m_hotbar.Clone();
    }

    /// <summary>
    /// ホットバーにおける特定スロットのアイテムを返すメソッド
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ScriptableObject GetSlot(int index)
    {
        if (index < 0 || index >= k_maxHotbarSize)
        {
            Debug.LogError("異常値が渡されました");
            return default;
        }

        return m_hotbar[index];
    }

    /// <summary>
    /// 現在の選択スロットを取得
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetSelectedSlot(int index)
    {
        return m_selectedIndex;
    }

    /// <summary>
    /// 現在の選択スロットを取得
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetSelectedSlot()
    {
        return m_selectedIndex;
    }


    /// <summary>
    /// 現在の選択スロットのBlockDataを取得
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BlockData GetSelectedBlockData()
    {
        return m_hotbar[m_selectedIndex];
    }

    /// <summary>
    /// 指定したスロットのBlockDataを取得
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BlockData GetBlockData(int index)
    {
        return m_hotbar[index];
    }

    /// <summary>
    /// 選択スロットの変更
    /// </summary>
    /// <param name="index"></param>
    public void SetSelectedSlot(int index)
    {
        m_selectedIndex = index;
    }

    /// <summary>
    /// 指定ホットバーのblockID書き換え
    /// </summary>
    /// <param name="index"></param>
    /// <param name="blockID"></param>
    public void SetSlotBlockID(int index, BlockData block)
    {
        m_hotbar[index] = block;
        OnSlotChanged?.Invoke(index);
    }

    
}
