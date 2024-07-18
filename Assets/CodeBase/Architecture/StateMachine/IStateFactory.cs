namespace CodeBase.Architecture.StateMachine
{
    public interface IStateFactory
    {
        public T CreateState<T>() where T : IGameState;
    }
}