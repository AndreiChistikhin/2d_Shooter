using System.Collections.Generic;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedAssetRequests =
            new();

        public AssetProvider()
        {
            Addressables.InitializeAsync();
        }
        
        public async UniTask<T> Load<T>(string address) where T : class
        {
            if (_cachedAssetRequests.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await LoadAsset(Addressables.LoadAssetAsync<T>(address), address);
        }

        public async UniTask<GameObject> Instantiate(string address)
        {
            var handle = Addressables.InstantiateAsync(address);
            return await handle.Task.AsUniTask();
        }

        private async UniTask<T> LoadAsset<T>(AsyncOperationHandle<T> handle, string cacheKey)
            where T : class
        {
            handle.Completed += completeHandle => { _cachedAssetRequests[cacheKey] = completeHandle; };

            return await handle.Task.AsUniTask();
        }
    }
}