using System.Linq;
using CodeBase.AssetManagement;
using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services
{
    public class StaticData : IStaticData
    {
        private IAssetProvider _assetProvider;

        public StaticData(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask<PlayerConfig> GetPlayerStaticData()
        {
            return await _assetProvider.Load<PlayerConfig>(AssetAddress.PlayerConfig);
        }

        public async UniTask<EnemyConfig> GetEnemyStaticData()
        {
            return await _assetProvider.Load<EnemyConfig>(AssetAddress.EnemyConfig);
        }
        
        public async UniTask<WorldConfig> GetWorldStaticData()
        {
            return await _assetProvider.Load<WorldConfig>(AssetAddress.WorldConfig);
        }

        public async UniTask<PopUpParameters> GetPopUpStaticData(PopUpId popUpId)
        {
            PopUpConfig popUpConfig = await _assetProvider.Load<PopUpConfig>(AssetAddress.WindowConfig);
            return popUpConfig.PopUps.FirstOrDefault(x => x.PopUpId == popUpId);
        }
    }
}