namespace CodeBase.Architecture.StateMachine
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}