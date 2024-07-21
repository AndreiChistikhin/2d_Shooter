using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PopUpBase : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _playAgain;
        [SerializeField] private float _animationTimeInSeconds;

        public event Action OnRestartPressed;
        
        private void Start()
        {
            _playAgain.onClick.AddListener(PlayAgain);
        }

        private void OnDestroy()
        {
            _playAgain.onClick.RemoveListener(PlayAgain);
        }

        public void Show()
        {
            _rectTransform.DOAnchorPos(Vector2.zero, _animationTimeInSeconds).SetEase(Ease.OutBounce);
        }

        private void PlayAgain()
        {
            Hide();
            OnRestartPressed?.Invoke();
        }

        private void Hide()
        {
            _rectTransform.DOAnchorPos(new Vector2(0, Screen.height), _animationTimeInSeconds);
        }
    }
}