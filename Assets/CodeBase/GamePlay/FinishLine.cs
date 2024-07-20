using CodeBase.Services.Interfaces;
using UnityEngine;
using Zenject;

public class FinishLine : MonoBehaviour
{
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyMovement enemyMovement))
            _gameFactory.Enemies.Release(enemyMovement.gameObject);
    }
}
