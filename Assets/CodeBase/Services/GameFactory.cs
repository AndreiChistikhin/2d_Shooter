using CodeBase.AssetManagement;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace CodeBase.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;

        private ObjectPool<GameObject> _enemies;

        public ObjectPool<GameObject> Enemies => _enemies;

        public GameFactory(DiContainer diContainer,
            IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;

            InitObjectPools().Forget();
        }

        private async UniTaskVoid InitObjectPools()
        {
            GameObject enemy = await _assetProvider.Load<GameObject>(AssetAddress.Enemy);

            _enemies = new ObjectPool<GameObject>(() =>
                {
                    GameObject instantiatedGameObject = Object.Instantiate(enemy);
                    _diContainer.InjectGameObject(instantiatedGameObject);

                    return instantiatedGameObject;
                },
                t =>
                {
                    t.SetActive(true);
                },
                t =>
                {
                    t.GetComponent<EnemyMovement>().StopMovement();
                    t.SetActive(false);
                });
        }

        public void ClearScene()
        {
        }

        public async UniTask<GameObject> CreatePlayer(Vector3 at)
        {
            return await InstantiatePrefab(AssetAddress.Player, at);
        }

        public async UniTask<GameObject> CreateHUD()
        {
            return await InstantiatePrefab(AssetAddress.Hud, Vector3.zero);
        }

        public async UniTask<GameObject> CreateSpawnerGroup(Vector3 at)
        {
            return await InstantiatePrefab(AssetAddress.SpawnerGroup, at);
        }

        public async UniTask<GameObject> CreateFinishLine(Vector3 at)
        {
            return await InstantiatePrefab(AssetAddress.FinishLine, at);
        }

        public async UniTask<GameObject> CreateEnemy(Vector3 at)
        {
            return await InstantiatePrefab(AssetAddress.Enemy, at);
        }

        public GameObject CreateBulletWithObjectPool()
        {
            return CreateBullet().GetAwaiter().GetResult();
        }

        private async UniTask<GameObject> CreateBullet()
        {
            return await InstantiatePrefab(AssetAddress.Bullet, Vector3.back);
        }

        private async UniTask<GameObject> InstantiatePrefab(string path, Vector3 at)
        {
            GameObject instantiatedGameObject =
                Object.Instantiate(await _assetProvider.Load<GameObject>(path), at, Quaternion.identity);
            _diContainer.InjectGameObject(instantiatedGameObject);

            return instantiatedGameObject;
        }
    }
}