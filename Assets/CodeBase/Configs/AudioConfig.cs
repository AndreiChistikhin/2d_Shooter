using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Audio", fileName = "Audio")]
    public class AudioConfig : ScriptableObject
    {
        public List<AudioParameters> Audios;
    }

    [Serializable]
    public class AudioParameters
    {
        public AudioId AudioId;
        public AudioClip AudioClip;
    }

    public enum AudioId
    {
        Unknown = 0,
        MainMusic = 1,
        LoseSound = 2,
        WinSound = 3
    }
}
