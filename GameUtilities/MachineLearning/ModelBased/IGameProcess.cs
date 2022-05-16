namespace GameUtilities.MachineLearning.ModelBased;

public interface IGameProcess<TState, TAction>
{
    public IEnumerable<TAction> GetLegalActions(TState gameState);
    public TState GetNextState(TState gameState, TAction action);
    public bool WinningState(TState gameState);
    public bool DrawState(TState gameState);
    public int GameScore(TState gameState);
    public ulong Key(TState gameState);
    public int MaxScore(TState gameState);
}