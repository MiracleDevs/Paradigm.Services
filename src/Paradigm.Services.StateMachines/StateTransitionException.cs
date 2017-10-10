using System;
using System.Runtime.CompilerServices;

namespace Paradigm.Services.StateMachines
{
    public class StateTransitionException<TState>: Exception where TState : IState<TState>
    {
        public StateTransitionException(IStateContext<TState> context, [CallerMemberName]string transitionName = null): base($"Can not {transitionName} {context.Name} when is {context.CurrentState.Name}.")
        {
        }
    }
}