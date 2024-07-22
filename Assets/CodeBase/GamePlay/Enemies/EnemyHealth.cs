using System;
using UnityEngine;

namespace CodeBase.GamePlay.Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimation _enemyAnimation;

        public event Action HealthChanged;

        public float Current { get; set; }

        public float Max { get; set; }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            _enemyAnimation.ShowTakenDamage();

            HealthChanged?.Invoke();
        }
    }
}