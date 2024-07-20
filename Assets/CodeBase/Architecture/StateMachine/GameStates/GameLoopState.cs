using System;
using CodeBase.Services.Interfaces;
using CodeBase.UI;

namespace CodeBase.Architecture.StateMachine.GameStates
{
    public class GameLoopState : IGameState
    {
        private IUIFactory _uiFactory;
        private IStateMachine _stateMachine;

        public GameLoopState(IStateMachine stateMachine, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
        }
        public void Enter()
        {
            foreach (PopUpBase popUp in _uiFactory.CreatedPopUps.Values)
                popUp.OnRestartPressed += MoveToLevelLoadState;
        }

        public void Exit()
        {
            foreach (PopUpBase popUp in _uiFactory.CreatedPopUps.Values)
                popUp.OnRestartPressed -= MoveToLevelLoadState;
        }

        private void MoveToLevelLoadState()
        {
            _stateMachine.Enter<LevelLoadState>();
        }
    }
}