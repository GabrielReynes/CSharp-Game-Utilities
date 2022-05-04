using System.Diagnostics;

namespace GameUtilities.MachineLearning.ModelBased;

public static class HeuristicSearch
{
    public static TAction MonteCarloTreeSearch<TState, TAction>
    (IGameTreeSearchProblem<TState, TAction> gameTreeSearchProblem, TState rootState,
        int maxDepth, int maximumExecutionTime) where TAction : struct
    {
        return MonteCarloTreeSearch(gameTreeSearchProblem, rootState, maxDepth,
            maximumExecutionTime, Math.Sqrt(2), new Random());
    }
    
    public static TAction MonteCarloTreeSearch<TState, TAction>
    (IGameTreeSearchProblem<TState, TAction> gameTreeSearchProblem, TState rootState,
        int maxDepth, int maximumExecutionTime, int seed) where TAction : struct
    {
        return MonteCarloTreeSearch(gameTreeSearchProblem, rootState, maxDepth,
            maximumExecutionTime, Math.Sqrt(2), new Random(seed));
    }
    
    public static TAction MonteCarloTreeSearch<TState, TAction>
    (IGameTreeSearchProblem<TState, TAction> gameTreeSearchProblem, TState rootState,
        int maxDepth, int maximumExecutionTime, double explorationParameter) where TAction : struct
    {
        return MonteCarloTreeSearch(gameTreeSearchProblem, rootState, maxDepth,
            maximumExecutionTime, explorationParameter, new Random());
    }
    
    private static TAction MonteCarloTreeSearch<TState, TAction>
        (IGameTreeSearchProblem<TState, TAction> gameTreeSearchProblem, TState rootState,
            int maxDepth, int maximumExecutionTime, double explorationParameter, Random random) 
        where TAction : struct
    {
        MonteCarloTreeNode<TAction> rootNode = new (default, null);
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < maximumExecutionTime)
        {
            MonteCarloTreeNode<TAction> actNode = rootNode;
            TState actState = rootState;
            while (actNode.Children.Count > 0)
            {
                actNode = actNode.SelectBestChild(explorationParameter);
                actState = gameTreeSearchProblem.GetNextState(actState, actNode.LastAction);
            }
            
            if (!gameTreeSearchProblem.EndState(actState))
            {
                foreach (TAction legalAction in gameTreeSearchProblem.GetLegalActions(actState))
                {
                    actNode.Children.Add(new MonteCarloTreeNode<TAction>(legalAction, actNode));
                }

                actNode = actNode.Children[random.Next(actNode.Children.Count)];
                actState = gameTreeSearchProblem.GetNextState(actState, actNode.LastAction);
            }

            for (int i = 0; i < maxDepth && !gameTreeSearchProblem.EndState(actState); i++)
            {
                TAction[] legalActions = gameTreeSearchProblem.GetLegalActions(actState).ToArray();
                TAction nextAction = legalActions[random.Next(legalActions.Length)];
                actState = gameTreeSearchProblem.GetNextState(actState, nextAction);
            }

            bool win = gameTreeSearchProblem.GameScore(actState) > 0;
            for (MonteCarloTreeNode<TAction> aux = actNode; aux.Parent != null; aux = aux.Parent!)
            {
                aux.NumberOfSimulations++;
                if (win) aux.NumberOfWins++;
            }
        }

        return rootNode.SelectBestChild(0d).LastAction;
    }
}