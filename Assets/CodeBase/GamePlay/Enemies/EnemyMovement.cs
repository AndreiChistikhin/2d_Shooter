using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.GamePlay.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;

        private EnemyConfig _enemyStaticData;
        private float _enemySpeed;

        [Inject]
        private async UniTaskVoid Construct(IStaticData staticData)
        {
            _enemyStaticData = await staticData.GetEnemyStaticData();
        }

        public void Move()
        {
            SetSpeed();
            StartEnemyMovement();
        }

        public void StopMovement() =>
            _rb.velocity = Vector2.zero;

        private void SetSpeed()
        {
            if (_enemyStaticData.MaxEnemiesSpeed < _enemyStaticData.MinEnemiesSpeed)
            {
                Debug.LogError(
                    "Максимальная скорость врагов меньше минимальной скорости, поправьте Scriptable object, " +
                    "скорость приравнвиается к Min");
                _enemySpeed = _enemyStaticData.MinEnemiesSpeed;
            }
            else
            {
                _enemySpeed = Random.Range(_enemyStaticData.MinEnemiesSpeed,
                    _enemyStaticData.MaxEnemiesSpeed);
            }
        }

        private void StartEnemyMovement() =>
            _rb.velocity = _enemySpeed * Vector2.down;
    }
}
