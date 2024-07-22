using CodeBase.Configs;
using CodeBase.GamePlay;
using CodeBase.Services;
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
        private readonly IKillsCounter _killsCounter;
        private readonly IGameFactory _factory;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticData _staticData;
        private readonly AudioService _audioService;

        public LevelLoadState(ISceneLoader sceneLoader, IStaticData staticData, IGameFactory factory,
            IUIFactory uiFactory,
            IStateMachine gameStateMachine, IKillsCounter killsCounter, AudioService audioService)
        {
            _audioService = audioService;
            _killsCounter = killsCounter;
            _staticData = staticData;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _factory = factory;
            _uiFactory = uiFactory;
        }

        public void Enter() =>
            _sceneLoader.LoadScene(LevelSceneName, OnLoaded);

        public void Exit() =>
            _sceneLoader.LoadingCurtain.Hide().Forget();

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitWorld();
            await InitAudio();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private async UniTask InitUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async UniTask InitWorld()
        {
            await InitPlayer();
            await InitHUD();
            await InitFinishLine();
            await InitSpawner();
            await InitKillCounter();
        }

        private async UniTask InitAudio() =>
            await _audioService.PlayMusic(AudioId.MainMusic, true);

        private async UniTask InitPlayer()
        {
            PlayerConfig playerStaticData = await _staticData.GetPlayerStaticData();
            GameObject player = await _factory.CreatePlayer(playerStaticData.InitialPlayerPosition);
            IHealth playerHealth = player.GetComponent<IHealth>();
            playerHealth.Max = playerHealth.Current = playerStaticData.PlayerHealth;
        }

        private async UniTask InitHUD() =>
            await _factory.CreateHUD();

        private async UniTask InitFinishLine()
        {
            WorldConfig worldStaticData = await _staticData.GetWorldStaticData();
            await _factory.CreateFinishLine(new Vector3(0, worldStaticData.FinishLineYPosition, 0));
        }

        private async UniTask InitSpawner()
        {
            WorldConfig worldStaticData = await _staticData.GetWorldStaticData();
            await _factory.CreateSpawnerGroup(new Vector3(0, worldStaticData.SpawnerLineYPosition, 0));
        }

        private async UniTask InitKillCounter() =>
            await _killsCounter.ResetCounter();
    }
}