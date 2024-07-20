using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Architecture
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loadingCurtain;
        [SerializeField] private float _fadeOutDurationInSeconds;
        
        public void Show()
        {
            gameObject.SetActive(true);
            _loadingCurtain.alpha = 1;
        }

        public async UniTaskVoid Hide()
        {
            await _loadingCurtain.DOFade(0, _fadeOutDurationInSeconds).AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }
    }
}