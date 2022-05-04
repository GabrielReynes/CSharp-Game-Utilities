namespace GameUtilities.MachineLearning.ModelFree.LearningAgents;

public abstract class LearningAgent<TState, TAction> where TAction : struct
{
        /*
         * epsilon : exploration probability
         * alpha : learning rate
         * discount : reward discount rate
         */
        public float Epsilon, Alpha, Discount;
        
        private readonly Random _random;

        protected LearningAgent(float epsilon, float alpha, float discount)
        {
            Epsilon = epsilon;
            Alpha = alpha;
            Discount = discount;
            _random = new Random();
        }

        protected LearningAgent(float epsilon, float alpha, float discount, int seed) :
            this(epsilon, alpha, discount)
        {
            _random = new Random(seed);
        }

        protected abstract float GetValue(TState state, TAction action);
        public abstract void UpdateValue(TState state, TAction action, TState nextState,
            IEnumerable<TAction> nextLegalActions, float reward);

        public TAction GetAction(TState state, TAction[] legalActions)
        {
            if (legalActions.Length == 0) return default;
            return _random.NextDouble() < Epsilon
                ? legalActions[_random.Next(legalActions.Length)]
                : ComputeAction(state, legalActions);
        }

        protected float UpdatedValue(TState state, TAction action, TState nextState, 
            IEnumerable<TAction> nextLegalActions, float reward)
        {
            return reward + Discount * ComputeBestValue(nextState, nextLegalActions) - GetValue(state, action);
        }

        private float ComputeBestValue(TState state, IEnumerable<TAction> legalActions)
        {
            return (from action in legalActions select GetValue(state, action)).Max();
        }

        private TAction ComputeAction(TState state, TAction[] legalActions)
        {
            float bestQValue = ComputeBestValue(state, legalActions);
            bool CompareToBestValue(TAction action) => CompareValues(GetValue(state, action), bestQValue);
            TAction[] bestActions = legalActions.Where(CompareToBestValue).ToArray();
            return bestActions[_random.Next(bestActions.Length)];
        }

        private static bool CompareValues(float qVal1, float qVal2)
        {
            const float tolerance = 1e-3f;
            return Math.Abs(qVal1 - qVal2) < tolerance;
        }
}