using CodeBase.Services.Interfaces;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class Spawner : MonoBehaviour
{
    private IGameFactory _gameFactory;
    private ObjectPool<EnemyMovement> _enemies;

    [Inject]
    private void Construct(IStaticData staticData, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void GetEnemy()
    {
        GameObject enemy = _gameFactory.Enemies.Get();
        
        enemy.transform.position = transform.position;
        enemy.GetComponent<EnemyMovement>().Move();
    }
}
