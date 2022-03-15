namespace GraphSearchUtilities.DataStructure;

internal interface ISearchStruct<T>
{
    public void Add(GraphNode<T> obj);
    public GraphNode<T> Remove();
    public bool Empty();
}