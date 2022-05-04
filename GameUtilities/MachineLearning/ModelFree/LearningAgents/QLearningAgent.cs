using GameUtilities.DataStructure;

namespace GameUtilities.MachineLearning.ModelFree.LearningAgents
{
    public class QLearningAgent<TState, TAction> : LearningAgent<TState, TAction>
        where TAction : struct, IEquatable<TAction> where TState : IEquatable<TState>
    {
        private readonly DefaultInitializer<Tuple<TState, TAction>, float> _qValues;

        public QLearningAgent(float epsilon, float alpha, float discount) : 
            base(epsilon, alpha, discount)
        {
            _qValues = new DefaultInitializer<Tuple<TState, TAction>, float>();
        }

        public QLearningAgent(float epsilon, float alpha, float discount, int seed) : 
            base(epsilon, alpha, discount, seed)
        {
            _qValues = new DefaultInitializer<Tuple<TState, TAction>, float>();
        }

        protected override float GetValue(TState state, TAction action)
        {
            return _qValues[new Tuple<TState, TAction>(state, action)];
        }

        public override void UpdateValue(TState state, TAction action, TState nextState, 
            IEnumerable<TAction> nextLegalActions, float reward)
        {
            _qValues[new Tuple<TState, TAction>(state, action)] +=
                Alpha * UpdatedValue(state, action, nextState, nextLegalActions, reward);
        }
    }
}