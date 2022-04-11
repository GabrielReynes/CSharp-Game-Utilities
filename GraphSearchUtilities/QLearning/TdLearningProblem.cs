namespace GraphSearchUtilities.QLearning;

public class TdLearningProblem<TState, TAction, TKey> : QLearningProblem<TState, TAction> where TKey : notnull
{

    public delegate Dictionary<TKey, float> FeatureExtractor(TState state, TAction action);

    public readonly FeatureExtractor GetFeatureVector;

    public TdLearningProblem(Func<TState, IEnumerable<TAction>> getLegalActions, Func<TState, TAction, float> getReward,
        Func<TState, TAction, TState> getNextState, Func<TState, bool> endState, FeatureExtractor featureExtractor)
        : base(getLegalActions, getReward, getNextState, endState)
    {
        GetFeatureVector = featureExtractor;
    }
}