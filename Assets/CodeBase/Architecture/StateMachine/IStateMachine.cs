namespace CodeBase.Architecture.StateMachine
{
    public interface IStateMachine
    {
        void RegisterState<T>(IGameState state) where T : IGameState;
        public void Enter<T>() where T : IGameState;
    }
}
