namespace Paradigm.Services.StateMachines
{
    public interface IStateContext<out TState> where TState: IState<TState>
    {
        TState CurrentState { get; }

        string Name { get; }

        void InitializeState();

        void SetState(int state);
    }
}