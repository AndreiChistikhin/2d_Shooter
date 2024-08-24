using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Interfaces
{
    public interface IGameFactory
    {
        UniTask<GameObject> CreatePlayer();
        UniTask<GameObject> CreateHUD();
        UniTask CreateSpawnerGroup();
        UniTask CreateFinishLine();
        UniTaskVoid CreateEnemyWithPool(Vector3 at);
        void ReleaseEnemy(GameObject bullet);
        UniTaskVoid CreateBulletWithPool(Vector3 at, Vector3 direction);
        void ReleaseBullet(GameObject bullet);
    }
}