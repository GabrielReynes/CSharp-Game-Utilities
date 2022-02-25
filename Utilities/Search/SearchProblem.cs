namespace Utilities.Search;

public class SearchProblem<T>
{
    public readonly Func<T> GetStartState;
    public readonly Func<T, T[]> NeighboursFetch;
    public readonly Predicate<T> EndStatePredicate;
    public Predicate<T> ObstaclePredicate;
    public Func<T, int> Heuristic;

    public SearchProblem(Func<T> getStartState, Func<T, T[]> neighboursFetch, Predicate<T> endStatePredicate)
    {
        GetStartState = getStartState;
        NeighboursFetch = neighboursFetch;
        EndStatePredicate = endStatePredicate;
        ObstaclePredicate = (obj) => false;
        Heuristic = (obj) => 0;
    }
}