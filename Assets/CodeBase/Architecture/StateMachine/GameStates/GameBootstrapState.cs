using CodeBase.Services.Interfaces;

namespace CodeBase.Architecture.StateMachine.GameStates
{
    public class GameBootstrapState : IGameState
    {
        private const string BootStrapSceneName = "Bootstrap";
        private ISceneLoader _sceneLoader;
        private IStateMachine _gameStateMachine;

        public GameBootstrapState(ISceneLoader sceneLoader, IStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter() =>
            _sceneLoader.LoadScene(BootStrapSceneName, EnterLoadProgressState);

        public void Exit() {}

        private void EnterLoadProgressState() =>
            _gameStateMachine.Enter<LevelLoadState>();
    }
}