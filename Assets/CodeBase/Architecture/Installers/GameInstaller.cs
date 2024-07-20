using CodeBase.Architecture.StateMachine;
using CodeBase.Services;
using CodeBase.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.Architecture
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
    
        public override void InstallBindings()
        {
            InstallLoadingObjects();
            InstallServices();
            InstallFactories();
        }

        private void InstallLoadingObjects()
        {
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IStaticData>().To<StaticData>().AsSingle();
            Container.Bind<IPopUpService>().To<PopUpService>().AsSingle();
        }

        private void InstallFactories()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
        }
    }
}