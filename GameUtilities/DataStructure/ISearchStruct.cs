namespace GameUtilities.DataStructure
{

    internal interface ISearchStruct<T>
    {
        public void Add(T obj);
        public T Remove();
        public bool Empty();
    }
}