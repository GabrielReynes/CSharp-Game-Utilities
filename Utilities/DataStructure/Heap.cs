using Utilities.Search;

namespace Utilities.DataStructure
{
    /*
     * Classic Heap. Takes HeapNodes always keeps the one with minimum cost on top.
     */
    internal class Heap<T> : ISearchStruct<T> {
    
        private readonly GraphNode<T>[] _items;
        public int Length { get; private set; }
        public bool Empty() => Length == 0;
    
        public Heap(int size) {
            _items = new GraphNode<T>[size];
            Length = 0;
        }
    
        public void Add(GraphNode<T> item) {
            _items[Length] = item;
            SortUp(item);
            Length++;
        }
    
        public GraphNode<T> Remove() {
            GraphNode<T> firstItem = _items[0];
            _items[0] = _items[--Length];
            SortDown(_items[0]);
            return firstItem;
        }
        
        private void SortDown(GraphNode<T> item)
        {
            int auxIndex = 0;
            while (true)
            {
                int childIndexLeft = auxIndex * 2 + 1;
                int childIndexRight = auxIndex * 2 + 2;

                if (childIndexLeft < Length)
                {
                    int swapIndex = childIndexLeft;

                    if (childIndexRight < Length)
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

        private void SortUp(GraphNode<T> item)
        {
            int auxIndex = Length;
            while (true)
            {
                int parentIndex = (auxIndex - 1) / 2;
                GraphNode<T> parentItem = _items[parentIndex];
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