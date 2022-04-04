using GraphSearchUtilities.DataStructure;

namespace GraphSearchUtilities.QLearning;

/*
 * epsilon : exploration probability
 * alpha : learning rate
 * discount : reward discount rate
 */
public class QLearningAgent<TState, TAction>
{
    private float _epsilon, _alpha, _discount;
    private readonly QLearningProblem<TState, TAction> _qLearningProblem;

    private readonly DefaultInitializer<Tuple<TState, TAction>, float> _qValues;

    private readonly Random _random;

    public QLearningAgent(float epsilon, float alpha, float discount,
        QLearningProblem<TState, TAction> qLearningProblem)
    {
        _epsilon = epsilon;
        _alpha = alpha;
        _discount = discount;
        _qLearningProblem = qLearningProblem;
        _qValues = new DefaultInitializer<Tuple<TState, TAction>, float>();
        _random = new Random();
    }

    public QLearningAgent(float epsilon, float alpha, float discount,
        QLearningProblem<TState, TAction> qLearningProblem, int seed) : this(epsilon, alpha, discount, qLearningProblem)
    {
        _random = new Random(seed);
    }

    public virtual float GetQValue(TState state, TAction action)
    {
        return _qValues[new Tuple<TState, TAction>(state, action)];
    }

    public TAction? GetAction(TState state)
    {
        TAction[] legalActions = _qLearningProblem.GetLegalActions(state).ToArray();
        if (legalActions.Length == 0) return default;
        return _random.NextSingle() < _epsilon
            ? legalActions[_random.Next(0, legalActions.Length)]
            : ComputeAction(state, legalActions);
    }

    public void UpdateQValue(TState state, TAction action)
    {
        TState nextState = _qLearningProblem.GetNextAction(state, action);
        float reward = _qLearningProblem.GetReward(state, action);
        _qValues[new Tuple<TState, TAction>(state, action)] -=
            _alpha * (GetQValue(state, action) - reward - _discount * ComputeBestValue(nextState));
    }

    private float ComputeBestValue(TState state)
    {
        return ComputeBestValue(state, _qLearningProblem.GetLegalActions(state));
    }
    
    private float ComputeBestValue(TState state, IEnumerable<TAction> legalActions)
    {
        return (from action in legalActions select GetQValue(state, action)).Max();
    }

    private TAction ComputeAction(TState state, TAction[] legalActions)
    {
        float bestQValue = ComputeBestValue(state, legalActions);
        bool CompareToBestValue(TAction action) => CompareQValues(GetQValue(state, action), bestQValue);
        TAction[] bestActions = legalActions.Where(CompareToBestValue).ToArray();
        return bestActions[_random.Next(0, bestActions.Length)];
    }

    private static bool CompareQValues(float qVal1, float qVal2)
    {
        const float tolerance = 1e-3f;
        return MathF.Abs(qVal1 - qVal2) < tolerance;
    }
}