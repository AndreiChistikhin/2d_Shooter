using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;

        public void SetHealthText(float hp) =>
            _healthText.text = $"Здоровье: {(int) hp}";
    }
}