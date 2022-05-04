namespace GameUtilities.DataStructure;

public static class LinqUtilities
{
    public static T? MaxBy<T>(this IEnumerable<T> items, Func<T, double> compFunc)
    {
        return items.Aggregate(
            new {Max = double.MinValue, Item = default(T)},
            (state, el) =>
            {
                double current = compFunc(el);
                return (current > state.Max ? new {Max = current, Item = el} : state!)!;
            }).Item;
    }
}