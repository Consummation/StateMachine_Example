using System;
using System.Collections.Generic;

namespace StateMachineRealization
{
    public interface IState
    {
        void Tick(float deltaTime);

        void FixedTick(float deltaTime);

        void OnStateEnter();

        void OnStateExit();

        void SetTransitions(List<Transition> transitions);

        void AddTransition(IState to, Func<bool> condition);

        List<Transition> GetTransitions();
    }
}
