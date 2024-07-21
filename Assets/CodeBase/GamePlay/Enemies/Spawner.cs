using CodeBase.Services.Interfaces;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class Spawner : MonoBehaviour
{
    private IGameFactory _gameFactory;
    private ObjectPool<EnemyMovement> _enemies;

    [Inject]
    private void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void GetEnemy()
    {
        _gameFactory.CreateEnemyWithPool(transform.position);
    }
}