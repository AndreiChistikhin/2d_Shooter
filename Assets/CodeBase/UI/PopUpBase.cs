using System;
using CodeBase.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public abstract class PopUpBase : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _playAgain;
        [SerializeField] private float _animationTimeInSeconds;

        protected AudioService _audioService;

        public event Action OnRestartPressed;

        [Inject]
        private void Construct(AudioService audioService)
        {
            _audioService = audioService;
            _playAgain.onClick.AddListener(PlayAgain);
        }

        private void OnDestroy() =>
            _playAgain.onClick.RemoveListener(PlayAgain);

        public void Show()
        {
            _rectTransform.DOAnchorPos(Vector2.zero, _animationTimeInSeconds).SetEase(Ease.OutBounce);
            PlayMusic();
        }

        public abstract void PlayMusic();

        private void PlayAgain()
        {
            Hide();
            OnRestartPressed?.Invoke();
        }

        private void Hide() =>
            _rectTransform.DOAnchorPos(new Vector2(0, Screen.height), _animationTimeInSeconds);
    }
}