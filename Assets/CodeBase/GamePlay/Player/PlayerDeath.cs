using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Player
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;

        private bool _isDead;
        private IPopUpService _popUpService;

        [Inject]
        private void Construct(IPopUpService popUpService)
        {
            _popUpService = popUpService;
        }

        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            
            _popUpService.Open(PopUpId.LoseGame);
        }
    }
}