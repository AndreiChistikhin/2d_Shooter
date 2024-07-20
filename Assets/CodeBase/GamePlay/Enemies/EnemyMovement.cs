using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    private EnemyConfig _enemyStaticData;
    private float _enemySpeed;

    [Inject]
    private async UniTaskVoid Construct(IStaticData staticData)
    {
        _enemyStaticData = await staticData.GetEnemyStaticData();
    }

    public void Move()
    {
        SetSpeed();
        StartEnemyMovement();
    }

    public void StopMovement()
    {
        transform.DOKill();
    }

    private void SetSpeed()
    {
        if (_enemyStaticData.MaxEnemiesSpeed < _enemyStaticData.MinEnemiesSpeed)
        {
            Debug.LogError(
                "Максимальная скорость врагов меньше минимальной скорости, поправьте Scriptable object, " +
                "скорость приравнвиается к Min");
            _enemySpeed = _enemyStaticData.MinEnemiesSpeed;
        }
        else
        {
            _enemySpeed = Random.Range(_enemyStaticData.MinEnemiesSpeed,
                _enemyStaticData.MaxEnemiesSpeed);
        }
    }

    private void StartEnemyMovement()
    {
        transform.DOMoveY(transform.position.y - 1, _enemySpeed).SetLoops(-1,LoopType.Incremental).SetSpeedBased().SetEase(Ease.Linear);
    }
}
