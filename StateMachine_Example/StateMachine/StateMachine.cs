namespace StateMachineRealization
{
    public class StateMachine
    {
        private IState _currentState;

        public StateMachine(IState initialState)
        {
            SetCurrentState(initialState);
        }

        public void Tick(float deltaTime)
        {
            var transitionIndex = IsTransitionsCondition();
            if (transitionIndex != -1)
            {
                var activeTransition = _currentState.GetTransitions()[transitionIndex];
                _currentState.OnStateExit();
                var nextState = activeTransition.GetStateTo();
                SetCurrentState(nextState);
            }

            _currentState?.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            _currentState?.FixedTick(deltaTime);
        }

        public IState SetCurrentState(IState state)
        {
            _currentState = state;
            state.OnStateEnter();

            return _currentState;
        }

        public IState GetCurrentState() => _currentState;

        private int IsTransitionsCondition()
        {
            var currentTransitions = _currentState.GetTransitions();

            for (var i = 0; i < currentTransitions.Count; i++)
            {
                var condition = currentTransitions[i].GetCondition();

                if (condition.Invoke())
                {
                    return i;
                }
            }

            return -1;
        }
    }
}