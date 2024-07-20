using CodeBase.Configs;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.Interfaces
{
    public interface IStaticData
    {
        UniTask<PlayerConfig> GetPlayerStaticData();
        UniTask<EnemyConfig> GetEnemyStaticData();
        UniTask<WorldConfig> GetWorldStaticData();
        UniTask<PopUpParameters> GetPopUpStaticData(PopUpId popUpId);
    }
}