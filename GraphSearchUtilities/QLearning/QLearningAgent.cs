using GraphSearchUtilities.DataStructure;

namespace GraphSearchUtilities.QLearning
{
    public class QLearningAgent<TState, TAction> : LearningAgent<TState, TAction> where TAction : struct
    {
        
        private readonly DefaultInitializer<Tuple<TState, TAction>, float> _qValues;

        public QLearningAgent(float epsilon, float alpha, float discount,
            ILearningProblem<TState, TAction> learningProblem) : base(epsilon, alpha, discount, learningProblem)
        {
            _qValues = new DefaultInitializer<Tuple<TState, TAction>, float>();
        }

        public QLearningAgent(float epsilon, float alpha, float discount,
            ILearningProblem<TState, TAction> learningProblem, int seed) : base(epsilon, alpha, discount,
            learningProblem, seed)
        {
            _qValues = new DefaultInitializer<Tuple<TState, TAction>, float>();
        }

        protected override float GetValue(TState state, TAction action)
        {
            return _qValues[new Tuple<TState, TAction>(state, action)];
        }

        public override void UpdateValue(TState state, TAction action)
        {
            TState nextState = LearningProblem.GetNextState(state, action);
            float reward = LearningProblem.GetReward(state, action);
            _qValues[new Tuple<TState, TAction>(state, action)] +=
                Alpha * UpdatedValue(state, action, nextState, reward);
        }
    }
}