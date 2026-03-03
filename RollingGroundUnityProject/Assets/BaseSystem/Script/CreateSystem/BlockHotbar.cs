using RollingGround;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// クリエイティブモードにおけるホットバーを管理するクラス
/// </summary>
public class BlockHotbar : SingletonMonoBehaviour<BlockHotbar>, IInputReceiver
{
    private const int k_maxHotbarSize = 9; //ホットバーの最大サイズ

    [SerializeField]
    private BlockData[] m_hotbar = new BlockData[k_maxHotbarSize]; //ホットバー
    private MGameInputManager m_gameInputManager;
    private int m_selectedIndex;    //選択中のスロット番号
    public event Action<int> OnSlotItemChanged; //スロット内itemの変更時イベント
    public event Action<int> OnSelectedSlotChanged; //選択スロットの変更時イベント

    private void Awake()
    {
        m_selectedIndex = 0;
        m_gameInputManager = GameObject.FindFirstObjectByType<MGameInputManager>();
        m_gameInputManager.AddRecieveObject(this);
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
        OnSlotItemChanged?.Invoke(index);
    }

    #region 入力処理

    public void OnSelectSlotChange(InputAction.CallbackContext context)
    {
        SetSelectedSlot(int.Parse(context.control.name) - 1);
        OnSelectedSlotChanged?.Invoke(int.Parse(context.control.name) - 1);
    }

    #endregion
}
