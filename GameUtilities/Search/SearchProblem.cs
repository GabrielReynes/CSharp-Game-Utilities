namespace GameUtilities.Search
{
    /*
     * Represents a problem which defines a set of method used to simulate a displacement inside your search graph.
     */
    public class SearchProblem<T>
    {
        public readonly Func<T> GetStartState; // Method which returns the starting state
        public readonly Func<T, IEnumerable<T>> NeighboursFetch; // Method which returns a set of neighbours given a state
        public Predicate<T> EndStatePredicate; // Method which determines whether a given state is an end state
        public Predicate<T> ObstaclePredicate; // Method which determines whether a given state is an obstacle to be avoided
        public Func<T, T, int> Cost; // Method which is used to measure the cost of displacement between two states
        public Func<T, int> Heuristic; // Method which is user to determine the heuristic cost between two states
        public Action<T, T> CoupleAction; // Method which will be called for every node/neighbour couple

        public SearchProblem(T startState, Func<T, IEnumerable<T>> neighboursFetch) : this(() => startState,
            neighboursFetch)
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
}