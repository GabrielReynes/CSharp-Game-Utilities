using System.Collections;
using GraphSearchUtilities.Search;

namespace GraphSearchUtilities.DataStructure;

internal class MyQueue<T> : ISearchStruct<T>
{
    private readonly Queue<GraphNode<T>> _queue;

    public MyQueue()
    {
        _queue = new Queue<GraphNode<T>>();
    }

    public void Add(GraphNode<T> obj)
    {
        _queue.Enqueue(obj);
    }

    public GraphNode<T> Remove()
    {
        return _queue.Dequeue();
    }

    public bool Empty()
    {
        return _queue.Count == 0;
    }
}