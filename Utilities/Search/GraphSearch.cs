using Utilities.DataStructure;

namespace Utilities.Search
{
    public static class GraphSearch
    {
        private static T[] ClassicSearch<T>(ISearchStruct<T> searchStruct, SearchProblem<T> searchProblem)
        {
            T startState = searchProblem.GetStartState();
            HashSet<T> seen = new HashSet<T>() {startState};
            searchStruct.Add(new GraphNode<T>(startState));
            while (!searchStruct.Empty())
            {
                GraphNode<T> elm = searchStruct.Remove();
                if (searchProblem.EndStatePredicate(elm.Element)) return elm.RetracePath();

                foreach (T neighbour in searchProblem.NeighboursFetch(elm.Element))
                {
                    if (searchProblem.ObstaclePredicate(neighbour) || seen.Contains(neighbour)) continue;

                    GraphNode<T> neighbourNode = new GraphNode<T>(neighbour, elm)
                    {
                        Cost = elm.Cost + 1,
                        Heuristic = searchProblem.Heuristic(neighbour)
                    };

                    searchStruct.Add(neighbourNode);
                    seen.Add(neighbour);
                }
            }

            throw new Exception("No solution was found");
        }

        public static T[] AStar<T>(SearchProblem<T> searchProblem, int heapSize)
        {
            return ClassicSearch(new Heap<T>(heapSize), searchProblem);
        }

        public static T[] BreadthFirstSearch<T>(SearchProblem<T> searchProblem)
        {
            return ClassicSearch(new MyQueue<T>(), searchProblem);
        }
        
        public static T[] DepthFirstSearch<T>(SearchProblem<T> searchProblem)
        {
            return ClassicSearch(new MyStack<T>(), searchProblem);
        }
    }
}