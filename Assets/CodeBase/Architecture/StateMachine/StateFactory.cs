using Zenject;
 
 namespace CodeBase.Architecture.StateMachine
 {
     public class StateFactory : IStateFactory
     {
         private IInstantiator _container;
 
         public StateFactory(IInstantiator container)
         {
             _container = container;
         }
 
         public T CreateState<T>() where T : IGameState
         {
             return _container.Instantiate<T>();
         }
     }
 }