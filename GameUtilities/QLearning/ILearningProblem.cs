namespace GameUtilities.QLearning;

public interface ILearningProblem<TState, TAction>
{
    IEnumerable<TAction> GetLegalActions(TState state);
    bool EndState(TState state);
    TState GetNextState(TState state, TAction action);
    float GetReward(TState state, TAction action);
}