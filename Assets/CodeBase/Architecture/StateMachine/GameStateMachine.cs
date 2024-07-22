using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Architecture.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states = new ();
        private IGameState _activeState;

        public void RegisterState<T>(IGameState state) where T : IGameState =>
            _states[typeof(T)] = state;

        public void Enter<T>() where T : IGameState 
        {
            IGameState state = ChangeState<T>();
            state?.Enter();
        }

        private IGameState ChangeState<T>() where T : IGameState
        {
            _activeState?.Exit();

            bool hasState = _states.TryGetValue(typeof(T), out IGameState gameState);

            if (!hasState)
            {
                Debug.LogError("Стейт, в который хочешь войти, не зарегистрирован в стейт машине");
                return null;
            }
            
            _activeState = gameState;

            return gameState;
        }
    }
}