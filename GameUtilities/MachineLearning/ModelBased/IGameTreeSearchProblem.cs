namespace GameUtilities.MachineLearning.ModelBased;

public interface IGameTreeSearchProblem<TState, TAction> : IGameProcess<TState, TAction>
{
    public int GameScore(TState state);
}