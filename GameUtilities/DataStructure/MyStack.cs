namespace GameUtilities.DataStructure
{
    internal class MyStack<T> : ISearchStruct<T>
    {
        private readonly Stack<T> _stack;

        public MyStack()
        {
            _stack = new Stack<T>();
        }

        public void Add(T obj)
        {
            _stack.Push(obj);
        }

        public T Remove()
        {
            return _stack.Pop();
        }

        public bool Empty()
        {
            return _stack.Count == 0;
        }
    }
}