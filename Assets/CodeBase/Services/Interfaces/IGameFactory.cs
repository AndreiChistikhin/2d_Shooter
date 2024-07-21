using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Interfaces
{
    public interface IGameFactory
    {
        void ClearScene();
        UniTask<GameObject> CreatePlayer(Vector3 at);
        UniTask<GameObject> CreateHUD();
        UniTask<GameObject> CreateSpawnerGroup(Vector3 at);
        UniTask<GameObject> CreateFinishLine(Vector3 at);
        UniTaskVoid CreateEnemyWithPool(Vector3 at);
        void ReleaseEnemy(GameObject bullet);
        UniTaskVoid CreateBulletWithPool(Vector3 at, Vector3 direction);
        void ReleaseBullet(GameObject bullet);
    }
}