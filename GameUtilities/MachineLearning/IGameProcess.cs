namespace GameUtilities.MachineLearning;

public interface IGameProcess<TState, TAction>
{
    IEnumerable<TAction> GetLegalActions(TState state);
    TState GetNextState(TState state, TAction action);
    bool EndState(TState state);
}