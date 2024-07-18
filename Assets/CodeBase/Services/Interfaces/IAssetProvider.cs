using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.Interfaces
{
    public interface IAssetProvider
    {
        UniTask<T> Load<T>(string address) where T : class;
        UniTask<GameObject> Instantiate(string address);
    }
}