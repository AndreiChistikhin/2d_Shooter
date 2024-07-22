using DG.Tweening;
using UnityEngine;

namespace CodeBase.GamePlay.Enemies
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private const float AnimationDuration = 0.3f;

        private void OnEnable()
        {
            _spriteRenderer.DOKill();
            _spriteRenderer.color = Color.white;
        }

        public void ShowTakenDamage()
        {
            _spriteRenderer.color = Color.white;

            _spriteRenderer.DOColor(Color.red, AnimationDuration).SetLoops(2, LoopType.Yoyo);
        }
    }
}