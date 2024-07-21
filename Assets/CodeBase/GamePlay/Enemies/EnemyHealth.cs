using System;
using CodeBase.GamePlay;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;

        public float Current { get; set; }

        public float Max { get; set; }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            
            HealthChanged?.Invoke();
        }

    }
}