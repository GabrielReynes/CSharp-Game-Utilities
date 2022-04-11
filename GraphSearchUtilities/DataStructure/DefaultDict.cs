namespace GraphSearchUtilities.DataStructure
{
    public class DefaultDict<TKey, TVal> where TKey : notnull
    {
        private readonly Dictionary<TKey, TVal> _dictionary;
        private readonly Func<TKey, TVal> _defaultMethod;

        public DefaultDict(Func<TKey, TVal> defaultMethod)
        {
            _dictionary = new Dictionary<TKey, TVal>();
            _defaultMethod = defaultMethod;
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        private TVal Get(TKey key)
        {
            if (!ContainsKey(key)) _dictionary.Add(key, _defaultMethod(key));
            return _dictionary[key];
        }

        public TVal this[TKey key]
        {
            get => Get(key);
            set => _dictionary[key] = value;
        }
    }

    public class DefaultInitializer<TKey, TVal> : DefaultDict<TKey, TVal> where TKey : notnull where TVal : struct
    {
        public DefaultInitializer() : base((TKey key) => default(TVal))
        {
        }
    }
}