namespace GraphSearchUtilities.DataStructure;

internal class GraphNode<T> : IComparable<GraphNode<T>>
{
    public readonly T Element;
    public readonly GraphNode<T>? Parent;
    public int Cost, Heuristic;
    public int FullCost => Cost + Heuristic;

    public GraphNode(T element)
    {
        Element = element;
        Parent = null;
        Cost = 0;
        Heuristic = 0;
    }
    
    public GraphNode(T element, GraphNode<T> parent) : this(element)
    {
        Parent = parent;
    }

    public int CompareTo(GraphNode<T>? other)
    {
        return FullCost - (other?.FullCost ?? 0);
    }

    public T[] RetracePath()
    {
        Stack<T> path = new Stack<T>();
        for (GraphNode<T>? aux = this; aux != null; aux = aux.Parent)
            path.Push(aux.Element);
        return path.ToArray();
    }
}