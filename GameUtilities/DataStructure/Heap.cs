namespace GameUtilities.DataStructure
{
    /// <summary>
    /// Classic Heap. Always keeps the node with minimum cost on top.
    /// </summary>
    /// <typeparam name="T">The type to store</typeparam>
    public class Heap<T> : ISearchStruct<T> where T : IComparable<T>
    {

        private T[] _items;
        private int _length;
        public bool Empty() => _length == 0;

        public Heap(int initialCapacity)
        {
            _items = new T[initialCapacity];
            _length = 0;
        }

        private void Resize()
        {
            Array.Resize(ref _items, _items.Length * 2);
        }

        public void Add(T item)
        {
            if (_length == _items.Length) Resize();
            _items[_length] = item;
            SortUp(item);
            _length++;
        }

        public T Remove()
        {
            T firstItem = _items[0];
            _items[0] = _items[--_length];
            SortDown(_items[0]);
            return firstItem;
        }

        private void SortDown(T item)
        {
            int auxIndex = 0;
            while (true)
            {
                int childIndexLeft = auxIndex * 2 + 1;
                int childIndexRight = auxIndex * 2 + 2;

                if (childIndexLeft < _length)
                {
                    int swapIndex = childIndexLeft;

                    if (childIndexRight < _length)
                    {
                        if (_items[childIndexLeft].CompareTo(_items[childIndexRight]) > 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }

                    if (item.CompareTo(_items[swapIndex]) > 0)
                    {
                        Swap(auxIndex, swapIndex);
                        auxIndex = swapIndex;
                    }
                    else return;
                }
                else return;
            }
        }

        private void SortUp(T item)
        {
            int auxIndex = _length;
            while (true)
            {
                int parentIndex = (auxIndex - 1) / 2;
                T parentItem = _items[parentIndex];
                if (item.CompareTo(parentItem) >= 0) break;
                Swap(auxIndex, parentIndex);
                auxIndex = parentIndex;
            }
        }

        private void Swap(int i1, int i2)
        {
            (_items[i1], _items[i2]) = (_items[i2], _items[i1]);
        }
    }

}