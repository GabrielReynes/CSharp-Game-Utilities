using Utilities.DataStructure;

namespace Utilities.Search
{
    public static class GraphSearch
    {
        private static T[] ClassicSearch<T>(ISearchStruct<T> searchStruct, SearchProblem<T> searchProblem,
            out HashSet<T> seenNodes, uint maxCost = uint.MaxValue, bool throwException = true)
        {
            T startState = searchProblem.GetStartState();
            seenNodes = new HashSet<T>();
            searchStruct.Add(new GraphNode<T>(startState));
            while (!searchStruct.Empty())
            {
                GraphNode<T> elm = searchStruct.Remove();
                if (seenNodes.Contains(elm.Element)) continue;
                if (searchProblem.EndStatePredicate(elm.Element)) return elm.RetracePath();
                seenNodes.Add(elm.Element);
                
                if (elm.Cost == maxCost) continue;

                foreach (T neighbour in searchProblem.NeighboursFetch(elm.Element))
                {
                    if (searchProblem.ObstaclePredicate(neighbour)) continue;

                    GraphNode<T> neighbourNode = new GraphNode<T>(neighbour, elm)
                    {
                        Cost = elm.Cost + 1,
                        Heuristic = searchProblem.Heuristic(neighbour)
                    };

                    searchStruct.Add(neighbourNode);
                }
            }

            if (throwException) throw new Exception("No path was found.");
            return null!;
        }

        private static T[] ClassicSearch<T>(ISearchStruct<T> searchStruct, SearchProblem<T> searchProblem, 
            uint maxCost = uint.MaxValue)
        {
            return ClassicSearch(searchStruct, searchProblem, out HashSet<T> _, maxCost);
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

        public static T[] Propagation<T>(SearchProblem<T> searchProblem, uint maxCost = uint.MaxValue)
        {
            ClassicSearch(new MyQueue<T>(), searchProblem, out HashSet<T> seenNodes, maxCost, false);
            return seenNodes.ToArray();
        }
    }
}