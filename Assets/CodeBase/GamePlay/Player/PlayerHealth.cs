using System;
using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
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