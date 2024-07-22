using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private ParticleSystem _deathFX;

        private IKillsCounter _killsCounter;
        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(IKillsCounter killsCounter, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _killsCounter = killsCounter;
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy() =>
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die().Forget();
        }

        private async UniTaskVoid Die()
        {
            Instantiate(_deathFX, transform.position, quaternion.identity);
            _killsCounter.AddKill();

            _gameFactory.ReleaseEnemy(gameObject);
        }
    }
}