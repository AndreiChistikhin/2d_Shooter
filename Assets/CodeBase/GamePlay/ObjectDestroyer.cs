using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class ObjectDestroyer : MonoBehaviour
    {
        [SerializeField] private int _destroyTimeInSeconds;

        private async void OnEnable()
        {
            await UniTask.Delay(_destroyTimeInSeconds * 1000);
            Destroy(gameObject);
        }
    }
}