using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Player", fileName = "Player")]
    public class PlayerConfig : ScriptableObject
    {
        public Vector3 InitialPlayerPosition;
        public float PlayerMovementSpeed;
        public float ShootingRadius;
        public float ShootingCoolDown;
        public int ShootingDamage;
        public float BulletSpeed;
        public int PlayerHealth;
    }
}