using GraphSearchUtilities.Search;

namespace GraphSearchUtilities.DataStructure
{
    internal class MyStack<T> : ISearchStruct<T>
    {
        private readonly Stack<GraphNode<T>> _stack;

        public MyStack()
        {
            _stack = new Stack<GraphNode<T>>();
        }

        public void Add(GraphNode<T> obj)
        {
            _stack.Push(obj);
        }

        public GraphNode<T> Remove()
        {
            return _stack.Pop();
        }

        public bool Empty()
        {
            return _stack.Count == 0;
        }
    }
}