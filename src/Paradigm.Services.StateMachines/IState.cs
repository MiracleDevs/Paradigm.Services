/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

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