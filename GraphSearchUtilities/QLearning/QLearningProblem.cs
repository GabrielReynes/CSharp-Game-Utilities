namespace GraphSearchUtilities.QLearning;

public class QLearningProblem<TState, TAction>
{
    public readonly Func<TState, IEnumerable<TAction>> GetLegalActions;
    public readonly Func<TState, TAction, float> GetReward;
    public readonly Func<TState, TAction, TState> GetNextAction;

    public QLearningProblem(Func<TState, IEnumerable<TAction>> getLegalActions, Func<TState, TAction, float> getReward,
        Func<TState, TAction, TState> getNextAction)
    {
        GetLegalActions = getLegalActions;
        GetReward = getReward;
        GetNextAction = getNextAction;
    }
}