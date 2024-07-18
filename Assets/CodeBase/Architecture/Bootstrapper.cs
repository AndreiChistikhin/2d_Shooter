using CodeBase.Architecture.StateMachine;
using CodeBase.Architecture.StateMachine.GameStates;
using UnityEngine;
using Zenject;

public class Bootstrapper : MonoBehaviour
{
    private IStateMachine _gameStateMachine;
    private IStateFactory _stateFactory;

    [Inject]
    private void Construct(IStateMachine gameStateMachine, IStateFactory stateFactory)
    {
        _gameStateMachine = gameStateMachine;
        _stateFactory = stateFactory;
    }

    private void Awake()
    {
        if (_gameStateMachine == null || _stateFactory == null)
        {
            Debug.LogError("Зависимости Project Context не проинджектились, игра не может начаться");
            return;
        }
        
        RegisterGameStates();
        _gameStateMachine.Enter<GameBootstrapState>();
    }

    private void RegisterGameStates()
    {
        _gameStateMachine.RegisterState<GameBootstrapState>(_stateFactory.CreateState<GameBootstrapState>());
        _gameStateMachine.RegisterState<LevelLoadState>(_stateFactory.CreateState<LevelLoadState>());
        _gameStateMachine.RegisterState<GameLoopState>(_stateFactory.CreateState<GameLoopState>());
    }
}