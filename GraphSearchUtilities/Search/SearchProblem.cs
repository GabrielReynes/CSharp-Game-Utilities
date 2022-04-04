namespace GraphSearchUtilities.Search;

public class SearchProblem<T>
{
    public readonly Func<T> GetStartState;
    public readonly Func<T, IEnumerable<T>> NeighboursFetch;
    public Predicate<T> EndStatePredicate;
    public Predicate<T> ObstaclePredicate;
    public Func<T, T, int> Cost;
    public Func<T, int> Heuristic;
    public Action<T, T> CoupleAction; //Method which will be called for every node/neighbour couple

    public SearchProblem(T startState, Func<T, IEnumerable<T>> neighboursFetch) : this(() => startState, neighboursFetch)
    {
    }

    public SearchProblem(Func<T> getStartState, Func<T, IEnumerable<T>> neighboursFetch)
    {
        GetStartState = getStartState;
        NeighboursFetch = neighboursFetch;
        EndStatePredicate = (obj) => false;
        ObstaclePredicate = (obj) => false;
        Cost = (obj1, obj2) => 1;
        Heuristic = (obj) => 0;
        CoupleAction = (obj1, obj2) => { };
    }
}