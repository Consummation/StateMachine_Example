using System;
using System.Collections.Generic;

namespace StateMachineRealization
{
    public abstract class State : IState
    {
        public State()
        {
            _transitions = new List<Transition>();
        }

        private List<Transition> _transitions;

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void FixedTick(float deltaTime)
        {
        }

        public void SetTransitions(List<Transition> transitions)
        {
            _transitions = transitions;
        }

        public void AddTransition(IState to, Func<bool> condition)
        {
            var transition = new Transition(to, condition);

            _transitions.Add(transition);
        }

        public List<Transition> GetTransitions() => _transitions;

        public virtual void OnStateEnter()
        {
        }

        public virtual void OnStateExit()
        {
        }
    }
}