namespace Utilities.Search;

public class SearchProblem<T>
{
    public readonly Func<T> GetStartState;
    public readonly Func<T, IEnumerable<T>> NeighboursFetch;
    public Predicate<T> EndStatePredicate;
    public Predicate<T> ObstaclePredicate;
    public Func<T, int> Heuristic;

    public SearchProblem(T startState, Func<T, IEnumerable<T>> neighboursFetch) : this(() => startState, neighboursFetch)
    {
    }

    public SearchProblem(Func<T> getStartState, Func<T, IEnumerable<T>> neighboursFetch)
    {
        GetStartState = getStartState;
        NeighboursFetch = neighboursFetch;
        EndStatePredicate = (obj) => false;
        ObstaclePredicate = (obj) => false;
        Heuristic = (obj) => 0;
    }
}