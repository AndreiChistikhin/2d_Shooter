using CodeBase.GamePlay;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthUI _healthText;

        private IHealth _heroHealth;
        
        [Inject]
        public void Construct(IHealth health)
        {
            _heroHealth = health;
            _heroHealth.HealthChanged += UpdateHpBar;
            
            UpdateHpBar();
        }
        
        private void OnDestroy()
        {
            if (_heroHealth != null)
                _heroHealth.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _healthText.SetHealthText(_heroHealth.Current);
        }
    }
}