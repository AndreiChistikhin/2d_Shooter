using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/World", fileName = "World")]
    public class WorldConfig : ScriptableObject
    {
        public float FinishLineYPosition;
        public float SpawnerLineYPosition;
    }
}