namespace GameUtilities.QLearning;

public abstract class LearningAgent<TState, TAction> where TAction : struct
{
        /*
         * epsilon : exploration probability
         * alpha : learning rate
         * discount : reward discount rate
         */
        public float Epsilon, Alpha, Discount;

        protected ILearningProblem<TState, TAction> LearningProblem;
        
        private readonly Random _random;

        protected LearningAgent(float epsilon, float alpha, float discount, 
            ILearningProblem<TState, TAction> learningProblem)
        {
            Epsilon = epsilon;
            Alpha = alpha;
            Discount = discount;
            LearningProblem = learningProblem;
            _random = new Random();
        }

        protected LearningAgent(float epsilon, float alpha, float discount,
            ILearningProblem<TState, TAction> learningProblem, int seed) :
            this(epsilon, alpha, discount, learningProblem)
        {
            _random = new Random(seed);
        }

        protected abstract float GetValue(TState state, TAction action);
        public abstract void UpdateValue(TState state, TAction action);

        public TAction GetAction(TState state)
        {
            TAction[] legalActions = LearningProblem.GetLegalActions(state).ToArray();
            if (legalActions.Length == 0) return default;
            return _random.NextDouble() < Epsilon
                ? legalActions[_random.Next(legalActions.Length)]
                : ComputeAction(state, legalActions);
        }
        
        public int Train(TState state, int maxIterations)
        {
            int i = 0;
            for (; i < maxIterations && !LearningProblem.EndState(state); i++)
            {
                TAction action = GetAction(state);
                UpdateValue(state, action);
                state = LearningProblem.GetNextState(state, action);
            }

            return i;
        }

        protected float UpdatedValue(TState state, TAction action, TState nextState, float reward)
        {
            return reward + Discount * ComputeBestValue(nextState) - GetValue(state, action);
        }
        
        private float ComputeBestValue(TState state)
        {
            return ComputeBestValue(state, LearningProblem.GetLegalActions(state));
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
            const float tolerance = 1e-1f;
            return Math.Abs(qVal1 - qVal2) < tolerance;
        }
}