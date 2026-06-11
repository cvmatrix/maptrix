namespace CVMatrix.DropOffDefense.SLib.Util.Collections;

internal static class Extensions
{
    public static MappedSetView<TOriginal, TMapped> MappedView<TOriginal, TMapped>(this IReadOnlySet<TOriginal> set, Func<TOriginal, TMapped> mapFunction, Func<TMapped, TOriginal?> comparisonFunction)
    {
        return new(set, mapFunction, comparisonFunction);
    }

    public static MappedSetView<TOriginal, TMapped> CastingView<TOriginal, TMapped>(this IReadOnlySet<TOriginal> set)
        where TOriginal : TMapped => MappedView(set, x => (TMapped)x, x => x is TOriginal v ? v : default);

    public static T GetOrInitialize<K, T>(this IDictionary<K, T> dictionary, K key, T initialValue)
    {
        if (dictionary.TryGetValue(key, out var v)) return v;
        dictionary[key] = initialValue;
        return initialValue;
    }
    public static T GetOrInitialize<K, T>(this IDictionary<K, T> dictionary, K key, Func<T> initializer)
    {
        if (dictionary.TryGetValue(key, out var v)) return v;
        dictionary[key] = initializer();
        return initializer();
    }
}