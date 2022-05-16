using System.Diagnostics;

namespace GameUtilities.MachineLearning.ModelBased;

public static class HeuristicSearch
{
    private const double SqrRootOfTwo = 1.41421356;

    public static TAction MonteCarloTreeSearch<TState, TAction>
    (IGameProcess<TState, TAction> gameProcess, TState rootState,
        int maxDepth, int maximumExecutionTime, int seed, double explorationParameter = SqrRootOfTwo)
        where TAction : struct
    {
        return MonteCarloTreeSearch(gameProcess, rootState, maxDepth,
            maximumExecutionTime, new Random(seed), explorationParameter);
    }

    public static TAction MonteCarloTreeSearch<TState, TAction>
    (IGameProcess<TState, TAction> gameProcess, TState rootState,
        int maxDepth, int maximumExecutionTime, double explorationParameter = SqrRootOfTwo) where TAction : struct
    {
        return MonteCarloTreeSearch(gameProcess, rootState, maxDepth,
            maximumExecutionTime, new Random(), explorationParameter);
    }

    private static TAction MonteCarloTreeSearch<TState, TAction>
    (IGameProcess<TState, TAction> gameProcess, TState rootState,
        int maxDepth, int maximumExecutionTime, Random random, double explorationParameter)
        where TAction : struct
    {
        MonteCarloTreeNode<TAction> rootNode = new(default, null);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < maximumExecutionTime)
        {
            MonteCarloTreeNode<TAction> actNode = rootNode;
            TState actState = rootState;
            while (actNode.Children.Count > 0)
            {
                MonteCarloTreeNode<TAction> test = actNode;
                actNode = actNode.SelectPromisingChild(explorationParameter);
                actState = gameProcess.GetNextState(actState, actNode.LastAction);
            }

            if (!gameProcess.WinningState(actState))
            {
                foreach (TAction legalAction in gameProcess.GetLegalActions(actState))
                {
                    actNode.Children.Add(new MonteCarloTreeNode<TAction>(legalAction, actNode));
                }

                actNode = actNode.Children[random.Next(actNode.Children.Count)];
                actState = gameProcess.GetNextState(actState, actNode.LastAction);
            }

            for (int i = 0; i < maxDepth && !gameProcess.WinningState(actState); i++)
            {
                TAction[] legalActions = gameProcess.GetLegalActions(actState).ToArray();
                TAction nextAction = legalActions[random.Next(legalActions.Length)];
                actState = gameProcess.GetNextState(actState, nextAction);
            }

            bool win = gameProcess.GameScore(actState) > 0;
            for (MonteCarloTreeNode<TAction>? aux = actNode; aux != null; aux = aux.Parent)
            {
                aux.NumberOfSimulations++;
                if (win) aux.NumberOfWins++;
            }
        }

        return rootNode.SelectBestChild().LastAction;
    }

    public static int Negamax<TState, TAction>(IGameProcess<TState, TAction> gameProcess,
        TState gameState, int maxDepth)
    {
        return Negamax(gameProcess, gameState, int.MinValue, int.MaxValue, 0, maxDepth);
    }

    private static int Negamax<TState, TAction>(IGameProcess<TState, TAction> gameProcess,
        TState gameState, int alpha, int beta, int depth, int maxDepth)
    {
        if (gameProcess.DrawState(gameState))
            return 0;

        foreach (TAction action in gameProcess.GetLegalActions(gameState))
        {
            TState nextState = gameProcess.GetNextState(gameState, action);
            if (gameProcess.WinningState(nextState))
                return gameProcess.GameScore(nextState);
        }

        int max = gameProcess.MaxScore(gameState);

        if (beta > max)
        {
            beta = max;
            if (alpha >= beta) return beta;
        }

        foreach (TAction action in gameProcess.GetLegalActions(gameState))
        {
            TState nextState = gameProcess.GetNextState(gameState, action);
            int score = -Negamax(gameProcess, nextState, -beta, -alpha, depth + 1, maxDepth);

            if (score >= beta) return score;
            if (score > alpha) alpha = score;
        }

        return alpha;
    }
}