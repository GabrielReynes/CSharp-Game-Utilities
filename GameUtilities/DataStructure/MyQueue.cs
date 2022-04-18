namespace GameUtilities.DataStructure
{

    internal class MyQueue<T> : ISearchStruct<T>
    {
        private readonly Queue<T> _queue;

        public MyQueue()
        {
            _queue = new Queue<T>();
        }

        public void Add(T obj)
        {
            _queue.Enqueue(obj);
        }

        public T Remove()
        {
            return _queue.Dequeue();
        }

        public bool Empty()
        {
            return _queue.Count == 0;
        }
    }
}