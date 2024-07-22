using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Services
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private IStaticData _staticData;

        [Inject]
        private void Construct(IStaticData staticData)
        {
            _staticData = staticData;
        }

        public async UniTask PlayMusic(AudioId audioId, bool loop)
        {
            AudioParameters audioStaticData = await _staticData.GetAudioStaticData(audioId);
            _audioSource.clip = audioStaticData.AudioClip;
            _audioSource.loop = loop;

            _audioSource.Play();
        }
    }
}