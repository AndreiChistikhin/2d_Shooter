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

        public async UniTask<PlayerConfig> ForPlayer()
        {
            return await _assetProvider.Load<PlayerConfig>(AssetAddress.PlayerConfig);
        }

        public async UniTask<EnemyConfig> ForSpawners()
        {
            return await _assetProvider.Load<EnemyConfig>(AssetAddress.EnemyConfig);
        }

        public async UniTask<WindowParameters> ForWindow(WindowId windowId)
        {
            WindowConfig windowConfig = await _assetProvider.Load<WindowConfig>(AssetAddress.WindowConfig);
            return windowConfig.Windows.FirstOrDefault(x => x.WindowId == windowId);
        }
    }
}