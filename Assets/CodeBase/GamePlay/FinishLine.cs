using CodeBase.GamePlay.Enemies;
using CodeBase.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay
{
    public class FinishLine : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private IHealth _playerHealth;

        [Inject]
        public void Construct(IGameFactory gameFactory, IHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _gameFactory = gameFactory;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out EnemyMovement enemyMovement))
            {
                _playerHealth.TakeDamage(1);
                enemyMovement.StopMovement();
                _gameFactory.ReleaseEnemy(enemyMovement.gameObject);
            }
        }
    }
}
