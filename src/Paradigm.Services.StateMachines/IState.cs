namespace Paradigm.Services.StateMachines
{
    public interface IState
    {
        
    }

    public interface IState<out TState>: IState 
        where TState : IState<TState>
    {
        IStateContext<TState> Context { get; }

        string Name { get; }
    }
}