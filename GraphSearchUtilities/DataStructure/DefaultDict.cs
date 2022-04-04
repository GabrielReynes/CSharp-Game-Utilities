namespace GraphSearchUtilities.DataStructure;

public class DefaultDict<TKey, TVal> where TKey : notnull
{
    private readonly Dictionary<TKey, TVal> _dictionary;
    private readonly Func<TKey, TVal> _defaultMethod;

    public DefaultDict(Func<TKey, TVal> defaultMethod)
    {
        _dictionary = new Dictionary<TKey, TVal>();
        _defaultMethod = defaultMethod;
    }

    public void Add(TKey key, TVal val)
    {
        _dictionary.Add(key, val);
    }

    public TVal Get(TKey key)
    {
        if (!_dictionary.ContainsKey(key)) _dictionary.Add(key, _defaultMethod(key));
        return _dictionary[key];
    }

    public TVal this[TKey key]
    {
        get => Get(key);
        set => Add(key, value);
    }
}

public class DefaultInitializer<TKey, TVal> : DefaultDict<TKey, TVal> where TKey : notnull where TVal : struct
{
    public DefaultInitializer() : base(default!)
    {
    }
}