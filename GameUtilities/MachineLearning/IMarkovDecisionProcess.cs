namespace GameUtilities.MachineLearning;

public interface IMarkovDecisionProcess<TState, TAction> : IGameProcess<TState, TAction>
{
    float GetReward(TState state, TAction action, TState nextState);
}