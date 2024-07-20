using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Interfaces
{
    public interface IGameFactory
    {
        ObjectPool<GameObject> Enemies { get; }
        void ClearScene();
        UniTask<GameObject> CreatePlayer(Vector3 at);
        UniTask<GameObject> CreateHUD();
        UniTask<GameObject> CreateSpawnerGroup(Vector3 at);
        UniTask<GameObject> CreateFinishLine(Vector3 at);
        GameObject CreateBulletWithObjectPool();
    }
}