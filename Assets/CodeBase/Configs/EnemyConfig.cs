using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Enemy", fileName = "Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        public int MinEnemiesAmountToWin;
        public int MaxEnemiesAmountToWin;
        public float MinEnemiesSpawnTimeOut;
        public float MaxEnemiesSpawnTimeout;
        public float MinEnemiesSpeed;
        public float MaxEnemiesSpeed;
        public int EnemiesHealth;
    }
}