using GameUtilities.DataStructure;

namespace GameUtilities.MachineLearning.ModelBased;

public class MonteCarloTreeNode<TAction>
    where TAction : struct
{
    public readonly TAction LastAction;
    public readonly MonteCarloTreeNode<TAction>? Parent;
    public readonly List<MonteCarloTreeNode<TAction>> Children;
    public int NumberOfWins, NumberOfSimulations;

    public MonteCarloTreeNode(TAction lastAction, MonteCarloTreeNode<TAction>? parent)
    {
        LastAction = lastAction;
        Parent = parent;
        Children = new List<MonteCarloTreeNode<TAction>>();
        NumberOfWins = 0;
        NumberOfSimulations = 0;
    }

    public MonteCarloTreeNode<TAction> SelectPromisingChild(double explorationParameter)
    {
        return Children.MaxBy(child => UctScore(child, explorationParameter))!;
    }
    
    public MonteCarloTreeNode<TAction> SelectBestChild()
    {
        return Children.Where(child => child.NumberOfSimulations > 0)
            .MaxBy(child => (double) child.NumberOfWins / NumberOfSimulations)!;
    }

    private double UctScore(MonteCarloTreeNode<TAction> child, double explorationParameter)
    {
        if (child.NumberOfSimulations == 0) return double.MaxValue;
        return (double) child.NumberOfWins / child.NumberOfSimulations 
               + explorationParameter * Math.Sqrt(Math.Log(NumberOfSimulations) / child.NumberOfSimulations);
    }
}