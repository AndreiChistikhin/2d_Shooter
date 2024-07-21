using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Services
{
    public class KillsCounter : IKillsCounter
    {
        private EnemyConfig _enemyStaticData;
        private readonly IStaticData _staticData;
        private IPopUpService _popUpService;

        private int _enemiesToKill;
        private int _currentKilledEnemies;

        public KillsCounter(IStaticData staticData, IPopUpService popUpService)
        {
            _popUpService = popUpService;
            _staticData = staticData;
        }

        public void AddKill()
        {
            _currentKilledEnemies++;
            
            if (_currentKilledEnemies >= _enemiesToKill)
                _popUpService.Open(PopUpId.WinGame);
        }

        public async UniTask ResetCounter()
        {
            _currentKilledEnemies = 0;
            await GetEnemiesAmountToWin();
        }

        private async UniTask GetEnemiesAmountToWin()
        {
            _enemyStaticData = await _staticData.GetEnemyStaticData();
            
            if (_enemyStaticData.MaxEnemiesSpeed < _enemyStaticData.MinEnemiesSpeed)
            {
                Debug.LogError(
                    "Максимальное количество враго для победы меньше, чем минимальное, поправьте Scriptable object, " +
                    "количество приравнвиается к Min");
                _enemiesToKill = _enemyStaticData.MinEnemiesAmountToWin;
            }
            else
            {
                _enemiesToKill = Random.Range(_enemyStaticData.MinEnemiesAmountToWin,
                    _enemyStaticData.MaxEnemiesAmountToWin + 1);
            }
            
            Debug.Log($"enemies to kill - {_enemiesToKill}");
        }
    }
}