using GraphSearchUtilities.DataStructure;

namespace GraphSearchUtilities.QLearning;

public class TdLearningAgent<TState, TAction, TKey> : LearningAgent<TState, TAction>
    where TAction : struct where TKey : notnull
{
    private readonly DefaultInitializer<TKey, float> _weights;

    private TdLearningProblem<TState, TAction, TKey> TdLearningProblem =>
        (LearningProblem as TdLearningProblem<TState, TAction, TKey>)!;

    private Dictionary<TKey, float> FeatureVector(TState state, TAction action) =>
        TdLearningProblem.GetFeatureVector(state, action);

    public TdLearningAgent(float epsilon, float alpha, float discount,
        TdLearningProblem<TState, TAction, TKey> learningProblem)
        : base(epsilon, alpha, discount, learningProblem)
    {
        _weights = new DefaultInitializer<TKey, float>();
    }

    public TdLearningAgent(float epsilon, float alpha, float discount,
        TdLearningProblem<TState, TAction, TKey> learningProblem, int seed)
        : base(epsilon, alpha, discount, learningProblem, seed)
    {
        _weights = new DefaultInitializer<TKey, float>();
    }

    protected override float GetValue(TState state, TAction action)
    {
        Dictionary<TKey, float> featureVector = FeatureVector(state, action);
        return featureVector.Keys.Select(key => _weights[key] * featureVector[key]).Sum();
    }

    public override void UpdateValue(TState state, TAction action)
    {
        TState nextState = LearningProblem.GetNextState(state, action);
        float reward = LearningProblem.GetReward(state, action);
        float updatedValue = UpdatedValue(state, action, nextState, reward);
        Dictionary<TKey, float> featureVector = FeatureVector(state, action);
        foreach (TKey key in featureVector.Keys)
        {
            _weights[key] += Alpha * updatedValue * featureVector[key];
        }
    }
}