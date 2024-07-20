using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Windows", fileName = "Windows")]
    public class PopUpConfig : ScriptableObject
    {
        public List<PopUpParameters> PopUps;
    }
    
    [Serializable]
    public class PopUpParameters
    {
        public PopUpId PopUpId;
        public GameObject PopUpPrefab;
    }
    
    public enum PopUpId
    {
        Unknown = 0,
        LoseGame = 1,
        WinGame = 2,
    }
}