namespace Utilities.Search;

public class SearchProblem<T>
{
    public readonly Func<T> GetStartState;
    public readonly Func<T, T[]> NeighboursFetch;
    public Predicate<T> EndStatePredicate;
    public Predicate<T> ObstaclePredicate;
    public Func<T, int> Heuristic;

    public SearchProblem(T startState, Func<T, T[]> neighboursFetch) : this(() => startState, neighboursFetch)
    {
    }

    public SearchProblem(Func<T> getStartState, Func<T, T[]> neighboursFetch)
    {
        GetStartState = getStartState;
        NeighboursFetch = neighboursFetch;
        EndStatePredicate = (obj) => false;
        ObstaclePredicate = (obj) => false;
        Heuristic = (obj) => 0;
    }
}