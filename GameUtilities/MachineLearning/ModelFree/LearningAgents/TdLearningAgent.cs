using GameUtilities.DataStructure;

namespace GameUtilities.MachineLearning.ModelFree.LearningAgents;

public class TdLearningAgent<TState, TAction, TKey> : LearningAgent<TState, TAction>
    where TAction : struct where TKey : notnull
{
    public delegate Dictionary<TKey, float> FeatureExtractor(TState state, TAction action);

    private readonly DefaultInitializer<TKey, float> _weights;
    private readonly FeatureExtractor _featureExtractor;
    
    public TdLearningAgent(float epsilon, float alpha, float discount, FeatureExtractor featureExtractor)
        : base(epsilon, alpha, discount)
    {
        _featureExtractor = featureExtractor;
        _weights = new DefaultInitializer<TKey, float>();
    }

    public TdLearningAgent(float epsilon, float alpha, float discount, int seed, FeatureExtractor featureExtractor)
        : base(epsilon, alpha, discount, seed)
    {
        _featureExtractor = featureExtractor;
        _weights = new DefaultInitializer<TKey, float>();
    }

    protected override float GetValue(TState state, TAction action)
    {
        Dictionary<TKey, float> featureVector = _featureExtractor(state, action);
        return featureVector.Keys.Select(key => _weights[key] * featureVector[key]).Sum();
    }

    public override void UpdateValue(TState state, TAction action, TState nextState, 
        IEnumerable<TAction> nextLegalActions, float reward)
    {
        float updatedValue = UpdatedValue(state, action, nextState, nextLegalActions, reward);
        Dictionary<TKey, float> featureVector = _featureExtractor(state, action);
        foreach (TKey key in featureVector.Keys)
        {
            _weights[key] += Alpha * updatedValue * featureVector[key];
        }
    }
}