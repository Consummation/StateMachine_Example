using System;

namespace StateMachineRealization
{
    public class Transition
    {
        private readonly IState to;
        private readonly Func<bool> condition;

        public Transition(IState to, Func<bool> condition)
        {
            this.to = to;
            this.condition = condition;
        }

        public Func<bool> GetCondition() => condition;
        public IState GetStateTo() => to;
    }
}