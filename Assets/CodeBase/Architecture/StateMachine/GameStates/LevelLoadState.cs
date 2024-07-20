using System.Threading.Tasks;
using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Architecture.StateMachine.GameStates
{
    public class LevelLoadState : IGameState
    {
        private const string LevelSceneName = "Game";
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _gameStateMachine;
        private IGameFactory _factory;
        private IUIFactory _uiFactory;
        private IStaticData _staticData;

        public LevelLoadState(ISceneLoader sceneLoader, IStaticData staticData, IGameFactory factory,
            IUIFactory uiFactory,
            IStateMachine gameStateMachine)
        {
            _staticData = staticData;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _factory = factory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _factory.ClearScene();
            _sceneLoader.LoadScene(LevelSceneName, OnLoaded);
        }

        public void Exit()
        {
            _sceneLoader.LoadingCurtain.Hide().Forget();
        }

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitWorld();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private async UniTask InitUIRoot()
        {
            await _uiFactory.CreateUIRoot();
        }

        private async UniTask InitWorld()
        {
            await InitPlayer();
            await InitHUD();
            await InitFinishLine();
            await InitSpawner();
        }

        private async UniTask InitPlayer()
        {
            PlayerConfig playerStaticData = await _staticData.GetPlayerStaticData();
            _factory.CreatePlayer(playerStaticData.InitialPlayerPosition);
        }

        private async UniTask InitHUD()
        {
            GameObject HUD = await _factory.CreateHUD();
            //Скорее всего надо будет проинджектить в DI Ihealth нашего перса
            //HUD.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }

        private async UniTask InitFinishLine()
        {
            WorldConfig worldStaticData = await _staticData.GetWorldStaticData();
            _factory.CreateFinishLine(new Vector3(0, worldStaticData.FinishLineYPosition, 0));
        }

        private async UniTask InitSpawner()
        {
            WorldConfig worldStaticData = await _staticData.GetWorldStaticData();
            _factory.CreateSpawnerGroup(new Vector3(0, worldStaticData.SpawnerLineYPosition, 0));
        }
    }
}