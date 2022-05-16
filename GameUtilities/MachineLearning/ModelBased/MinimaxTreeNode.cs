namespace GameUtilities.MachineLearning.ModelBased;

public class MinimaxTreeNode<TState, TAction> where TAction : struct where TState : notnull
{
    public readonly int Depth;
    public readonly bool Maximizing;
    public readonly TState GameState;
    public readonly MinimaxTreeNode<TState, TAction>? Parent;
    public int Alpha, Beta;
    
    private int _bestGameScore;
    public TAction ChosenAction { get; private set; }

    public MinimaxTreeNode(int depth, bool maximizing, TState gameState, 
        MinimaxTreeNode<TState, TAction>? parent)
    {
        Depth = depth;
        Maximizing = maximizing;
        GameState = gameState;
        Parent = parent;
        _bestGameScore = maximizing ? int.MinValue : int.MaxValue;
        Alpha = int.MinValue;
        Beta = int.MaxValue;
        ChosenAction = default;
    }

    public void ProcessGameValue(int gameScore, TAction legalAction, Dictionary<TState, int> transitionTable)
    {
        for (MinimaxTreeNode<TState, TAction>? aux = this; aux is not null; aux = aux.Parent)
        {
            if (aux.Maximizing && gameScore <= aux._bestGameScore || 
                !aux.Maximizing && gameScore >= aux._bestGameScore) break;
            aux._bestGameScore = gameScore;
            aux.ChosenAction = legalAction;
            transitionTable[aux.GameState] = gameScore;
            if (aux.Maximizing) aux.Alpha = Math.Max(aux.Alpha, aux._bestGameScore);
            else aux.Beta = Math.Min(aux.Beta, aux._bestGameScore);
        }
    }
}