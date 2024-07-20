using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SpawnerGroup : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;

    private EnemyConfig _enemyStaticData;
    private float _spawnTimeLeft;
    private bool _isInitialized;

    [Inject]
    private async UniTaskVoid Construct(IStaticData staticData)
    {
        _enemyStaticData = await staticData.GetEnemyStaticData();
        
        SetSpawnTimer();
        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized)
            return;

        if (_spawnTimeLeft > 0)
        {
            _spawnTimeLeft -= Time.deltaTime;
            return;
        }

        int randomIndex = Random.Range(0, _spawners.Count);
        _spawners[randomIndex].GetEnemy();
        SetSpawnTimer();
    }

    private void SetSpawnTimer()
    {
        if (_enemyStaticData.MaxEnemiesSpawnTimeout < _enemyStaticData.MinEnemiesSpawnTimeOut)
        {
            Debug.LogError(
                "Максимальное время спавна врагов меньше минимального времени, поправьте Scriptable object, " +
                "время спавна приравнвиается к MinEnemiesSpawnTimeOut");
            _spawnTimeLeft = _enemyStaticData.MinEnemiesSpawnTimeOut;
        }
        else
        {
            _spawnTimeLeft = Random.Range(_enemyStaticData.MinEnemiesSpawnTimeOut,
                _enemyStaticData.MaxEnemiesSpawnTimeout);
        }
    }
}