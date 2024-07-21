using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using CodeBase.GamePlay.BulletNameSpace;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _shootingObserver;

        private PlayerConfig _playerConfig;
        private IGameFactory _gameFactory;

        private List<GameObject> _shootingTargets = new();
        private float _timeToNextShot;
        private bool _isInitialized;

        private bool CanShoot => _timeToNextShot <= 0 && _shootingTargets.Count > 0;

        [Inject]
        private async UniTaskVoid Construct(IStaticData staticData, IGameFactory gameFactory)
        {
            _playerConfig = await staticData.GetPlayerStaticData();
            _gameFactory = gameFactory;

            _shootingObserver.CircleCollider2D.radius = _playerConfig.ShootingRadius;
            _shootingObserver.OnTriggerEnter += TrySetEnemyAsShootingTarget;
            _shootingObserver.OnTriggerExit += TryRemoveEnemyAsShootingTarget;

            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;

            if (_timeToNextShot > 0)
                _timeToNextShot -= Time.deltaTime;
            
            if (!CanShoot)
                return;

            Shoot();
            _timeToNextShot = _playerConfig.ShootingCoolDown;
        }

        private void OnDestroy()
        {
            _shootingObserver.OnTriggerEnter -= TrySetEnemyAsShootingTarget;
            _shootingObserver.OnTriggerExit -= TryRemoveEnemyAsShootingTarget;
        }

        private void TrySetEnemyAsShootingTarget(Collider2D obj)
        {
            if (obj.TryGetComponent(out EnemyMovement enemyMovement))
                _shootingTargets.Add(obj.gameObject);
        }

        private void TryRemoveEnemyAsShootingTarget(Collider2D obj)
        {
            if (obj.TryGetComponent(out EnemyMovement enemyMovement)) 
                _shootingTargets.Remove(obj.gameObject);
        }

        private void Shoot()
        {
            GameObject closestEnemy = IdentifyClosestEnemy();

            ShootBullet(closestEnemy);
        }

        private GameObject IdentifyClosestEnemy()
        {
            List<GameObject> orderedEnemiesByDistance = _shootingTargets
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).ToList();
            GameObject closestEnemy = orderedEnemiesByDistance[0];
            
            return closestEnemy;
        }

        private void ShootBullet(GameObject closestEnemy)
        {
            _gameFactory.CreateBulletWithPool(transform.position, closestEnemy.transform.position - transform.position);
        }
    }
}