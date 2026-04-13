using UnityEngine;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Addressablesを用いた安全なロード管理クラス
/// ・重複ロード防止
/// ・キャッシュ
/// ・アンロード
/// ・アドレス検証対応
/// </summary>
public class AddressableLoader : SingletonMonoBehaviour<AddressableLoader>
{
    // 現在ロード中のアドレスを追跡
    private readonly Dictionary<string, UniTask<GameObject>> m_loadingTasks = new();

    // キャッシュされたロード済みアセット
    private readonly Dictionary<string, GameObject> m_loadedAssets = new();

    // Addressablesのハンドル管理（アンロード用）
    private readonly Dictionary<string, AsyncOperationHandle<GameObject>> m_handles = new();

    /// <summary>
    /// Addressが有効かどうかを事前に検証します。
    /// </summary>
    public async UniTask<bool> ValidateAddressAsync(string address)
    {
        try
        {
            var locHandle = Addressables.LoadResourceLocationsAsync(address);
            await locHandle.Task;

            bool isValid = locHandle.Status == AsyncOperationStatus.Succeeded &&
                           locHandle.Result != null &&
                           locHandle.Result.Count > 0;

            Addressables.Release(locHandle);
            return isValid;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Assetの非同期ロード（キャッシュ＋重複防止＋検証付き）
    /// </summary>
    public async UniTask<GameObject> LoadAsync(string address)
    {
        // キャッシュ済みなら即返す
        if (m_loadedAssets.TryGetValue(address, out var cached))
            return cached;

        // すでにロード中なら待機
        if (m_loadingTasks.TryGetValue(address, out var existingTask))
            return await existingTask;

        // アドレスが有効かチェック
        bool valid = await ValidateAddressAsync(address);
        if (!valid)
            throw new Exception($"Invalid address: {address}");

        // 新しいロード開始
        var tcs = new UniTaskCompletionSource<GameObject>();
        m_loadingTasks[address] = tcs.Task;

        try
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var result = handle.Result;
                m_loadedAssets[address] = result;
                m_handles[address] = handle;
                tcs.TrySetResult(result);
                return result;
            }
            else
            {
                throw new Exception($"Failed to load prefab: {address}");
            }
        }
        catch (Exception e)
        {
            tcs.TrySetException(e);
            throw;
        }
        finally
        {
            m_loadingTasks.Remove(address);
        }
    }

    /// <summary>
    /// 指定したアドレスのAssetをアンロードします。
    /// </summary>
    public void Unload(string address)
    {
        if (m_handles.TryGetValue(address, out var handle))
        {
            Addressables.Release(handle);
            m_handles.Remove(address);
        }

        m_loadedAssets.Remove(address);
    }

    /// <summary>
    /// すべてのキャッシュ済みAssetをアンロードします。
    /// </summary>
    public void UnloadAll()
    {
        foreach (var handle in m_handles.Values)
        {
            Addressables.Release(handle);
        }

        m_handles.Clear();
        m_loadedAssets.Clear();
    }

    /// <summary>
    /// キャッシュされているか確認
    /// </summary>
    public bool IsLoaded(string address) => m_loadedAssets.ContainsKey(address);
}
