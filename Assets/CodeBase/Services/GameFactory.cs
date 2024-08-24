using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.Configs;
using CodeBase.GamePlay;
using CodeBase.GamePlay.BulletNameSpace;
using CodeBase.GamePlay.Enemies;
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
        private readonly IStaticData _staticData;
        private readonly DiContainer _diContainer;

        private ObjectPool<GameObject> _enemiesPool;
        private ObjectPool<GameObject> _bulletsPool;

        private GameObject _player;
        private List<GameObject> _gameObjectsOnAScene = new ();
        
        private Transform _rootGameObject;

        public GameFactory(DiContainer diContainer, IStaticData staticData,
            IAssetProvider assetProvider, IPopUpService popUpService)
        {
            _staticData = staticData;
            _assetProvider = assetProvider;
            _diContainer = diContainer;

            popUpService.OnPopUp += ClearScene;

            InitObjectPools().Forget();
        }

        private void ClearScene()
        {
            ClearBindings();

            _enemiesPool?.Clear();
            _bulletsPool?.Clear();

            foreach (GameObject gameObject in _gameObjectsOnAScene)
            {
                Object.Destroy(gameObject);
            }
            _gameObjectsOnAScene.Clear();
            
            _assetProvider.Cleanup();
        }

        public async UniTask<GameObject> CreatePlayer()
        {
            PlayerConfig playerStaticData = await _staticData.GetPlayerStaticData();
            
            _player = await LoadPrefabToScene(AssetAddress.Player, playerStaticData.InitialPlayerPosition);
            
            IHealth playerHealth = _player.GetComponent<IHealth>();
            playerHealth.Max = playerHealth.Current = playerStaticData.PlayerHealth;
            _diContainer.Bind<IHealth>().FromInstance(playerHealth).AsTransient();
            
            return _player;
        }

        public async UniTask<GameObject> CreateHUD() =>
            await LoadPrefabToScene(AssetAddress.Hud, Vector3.zero);

        public async UniTask CreateSpawnerGroup()
        {
            WorldConfig worldConfig = await _staticData.GetWorldStaticData();
            await LoadPrefabToScene(AssetAddress.SpawnerGroup, new Vector3(0, worldConfig.SpawnerLineYPosition, 0));
        }

        public async UniTask CreateFinishLine()
        {
            WorldConfig worldConfig = await _staticData.GetWorldStaticData();
            await LoadPrefabToScene(AssetAddress.FinishLine, new Vector3(0, worldConfig.FinishLineYPosition, 0));
        }

        public async UniTaskVoid CreateEnemyWithPool(Vector3 at)
        {
            EnemyConfig enemyStaticData = await _staticData.GetEnemyStaticData();
            GameObject enemy = _enemiesPool.Get();
        
            enemy.transform.position = at;
            enemy.GetComponent<EnemyMovement>().Move();
            
            IHealth enemyHealth = enemy.GetComponent<IHealth>();
            enemyHealth.Current = enemyHealth.Max = enemyStaticData.EnemiesHealth;
        }

        public void ReleaseEnemy(GameObject enemy) =>
            _enemiesPool.Release(enemy);

        public async UniTaskVoid CreateBulletWithPool(Vector3 at, Vector3 direction)
        {
            PlayerConfig playerStaticData = await _staticData.GetPlayerStaticData();
            GameObject bulletPrefab = _bulletsPool.Get();

            Bullet bullet = bulletPrefab.GetComponent<Bullet>();
            bullet.Init(this, playerStaticData.BulletSpeed,
                direction, playerStaticData.ShootingDamage, at);
            bullet.StartMovement();
        }

        public void ReleaseBullet(GameObject bullet) =>
            _bulletsPool.Release(bullet);

        private void CreateGameObjectsRoot()
        {
            _rootGameObject = Object.Instantiate(new GameObject("Root")).transform;
        }

        private void ClearBindings() =>
            _diContainer.Unbind<IHealth>();

        private async UniTask InitObjectPools()
        {
            _enemiesPool = await InitObjectPool(AssetAddress.Enemy);
            _bulletsPool = await InitObjectPool(AssetAddress.Bullet);
        }

        private async UniTask<ObjectPool<GameObject>> InitObjectPool(string prefabPath)
        {
            GameObject loadedPrefab = await _assetProvider.Load<GameObject>(prefabPath);

            return new ObjectPool<GameObject>(
                () => InstantiatePrefab(loadedPrefab, Vector3.zero),
                t => t.SetActive(true),
                t => t.SetActive(false),
                Object.Destroy,
                false,
                10, 100);
        }

        private async UniTask<GameObject> LoadPrefabToScene(string path, Vector3 at)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(path);
            return InstantiatePrefab(prefab, at);
        }

        private GameObject InstantiatePrefab(GameObject prefab, Vector3 at)
        {
            if (_rootGameObject == null)
                CreateGameObjectsRoot();

            GameObject instantiatedPrefab = _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, _rootGameObject.transform);
            _gameObjectsOnAScene.Add(instantiatedPrefab);
            return instantiatedPrefab;
        }
    }
}