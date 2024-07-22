using CodeBase.Services.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.GamePlay.BulletNameSpace
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletOnSceneMaxTime;
        [SerializeField] private Rigidbody2D _rb;

        private IGameFactory _gameFactory;
        
        private float _bulletSpeed;
        private Vector3 _direction;
        private float _bulletDamage;
        private float _bulletOnSceneCurrentTime;

        public void Init(IGameFactory gameFactory, float bulletSpeed, Vector3 direction, float bulletDamage, Vector3 _initialPosition)
        {
            _gameFactory = gameFactory;
            
            _bulletDamage = bulletDamage;
            _direction = direction;
            _bulletSpeed = bulletSpeed;
            transform.position = _initialPosition;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IHealth enemyHealth))
            {
                enemyHealth.TakeDamage(_bulletDamage);
                HideBullet();
            }
        }

        private void Update()
        {
            if (gameObject.activeInHierarchy)
                _bulletOnSceneCurrentTime += Time.deltaTime;
            
            if (_bulletOnSceneCurrentTime >= _bulletOnSceneMaxTime)
                HideBullet();
        }

        public void StartMovement()
        {
            transform.up = _direction.normalized;

            _rb.velocity = _direction.normalized * _bulletSpeed;
        }

        private void HideBullet()
        {
            _bulletOnSceneCurrentTime = 0;
            StopMovement();
            _gameFactory.ReleaseBullet(gameObject);
        }

        private void StopMovement() =>
            _rb.velocity = Vector2.zero;
    }
}