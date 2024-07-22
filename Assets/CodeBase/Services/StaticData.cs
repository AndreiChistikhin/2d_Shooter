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

        public async UniTask<PlayerConfig> GetPlayerStaticData() =>
            await _assetProvider.Load<PlayerConfig>(AssetAddress.PlayerConfig);

        public async UniTask<EnemyConfig> GetEnemyStaticData() =>
            await _assetProvider.Load<EnemyConfig>(AssetAddress.EnemyConfig);

        public async UniTask<WorldConfig> GetWorldStaticData() =>
            await _assetProvider.Load<WorldConfig>(AssetAddress.WorldConfig);

        public async UniTask<PopUpParameters> GetPopUpStaticData(PopUpId popUpId)
        {
            PopUpConfig popUpConfig = await _assetProvider.Load<PopUpConfig>(AssetAddress.WindowConfig);
            return popUpConfig.PopUps.FirstOrDefault(x => x.PopUpId == popUpId);
        }

        public async UniTask<AudioParameters> GetAudioStaticData(AudioId audioId)
        {
            AudioConfig audioConfig = await _assetProvider.Load<AudioConfig>(AssetAddress.AudioConfig);
            return audioConfig.Audios.FirstOrDefault(x => x.AudioId == audioId);
        }
    }
}