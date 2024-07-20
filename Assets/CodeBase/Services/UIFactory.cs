using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticData _staticData;
        private readonly IInstantiator _diContainer;

        private Transform _uiRoot;
        private Dictionary<PopUpId, PopUpBase> _createdWindows = new();

        public Dictionary<PopUpId, PopUpBase> CreatedPopUps => _createdWindows;

        public UIFactory(IAssetProvider assets, IStaticData staticData, DiContainer diContainer)
        {
            _assets = assets;
            _staticData = staticData;
            _diContainer = diContainer;
        }

        public async UniTask CreateUIRoot()
        {
            GameObject instantiate = await _assets.Instantiate(AssetAddress.UIRootPath);
            _uiRoot = instantiate.transform;
        }

        public void CreateAllPopUps()
        {
            CreateWindow(PopUpId.LoseGame).Forget();
            CreateWindow(PopUpId.WinGame).Forget();
        }

        private async UniTask CreateWindow(PopUpId popUpId)
        {
            PopUpParameters popUpParameters = await _staticData.GetPopUpStaticData(popUpId);
            PopUpBase popUp = _diContainer.InstantiatePrefabForComponent<PopUpBase>(popUpParameters.PopUpPrefab, _uiRoot);

            _createdWindows[popUpId] = popUp;
        }
    }
}