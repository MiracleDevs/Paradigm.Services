/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

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