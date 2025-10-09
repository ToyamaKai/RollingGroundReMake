using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Addressableを用いたロード処理
/// </summary>
public class AddressableLoader : MonoBehaviour
{

    /// <summary>
    /// Assetの非同期ロード
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async UniTask<GameObject> LoadAsync(string address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(address);
        await handle.Task;

        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            throw new Exception($"Failed to load prefab: {address}");
        }
    }
}
