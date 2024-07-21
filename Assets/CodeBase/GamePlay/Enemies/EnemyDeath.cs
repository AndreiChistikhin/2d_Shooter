using System;
using CodeBase.Enemy;
using CodeBase.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        
        private IKillsCounter _killsCounter;

        [Inject]
        private void Construct(IKillsCounter killsCounter)
        {
            _killsCounter = killsCounter;
            _health.HealthChanged += OnHealthChanged;
        }
        
        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _killsCounter.AddKill();
            gameObject.SetActive(false);
        }
    }
}