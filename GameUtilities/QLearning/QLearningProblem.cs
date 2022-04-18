namespace GameUtilities.QLearning
{
    public class QLearningProblem<TState, TAction> : ILearningProblem<TState, TAction>
    {
        private readonly Func<TState, IEnumerable<TAction>> _getLegalActions;
        private readonly Func<TState, TAction, float> _getReward;
        private readonly Func<TState, TAction, TState> _getNextState;
        private readonly Func<TState, bool> _endState;

        public QLearningProblem(Func<TState, IEnumerable<TAction>> getLegalActions,
            Func<TState, TAction, float> getReward,
            Func<TState, TAction, TState> getNextState, Func<TState, bool> endState)
        {
            _getLegalActions = getLegalActions;
            _getReward = getReward;
            _getNextState = getNextState;
            _endState = endState;
        }

        public IEnumerable<TAction> GetLegalActions(TState state)
        {
            return _getLegalActions(state);
        }

        public bool EndState(TState state)
        {
            return _endState(state);
        }

        public TState GetNextState(TState state, TAction action)
        {
            return _getNextState(state, action);
        }

        public float GetReward(TState state, TAction action)
        {
            return _getReward(state, action);
        }
    }
}