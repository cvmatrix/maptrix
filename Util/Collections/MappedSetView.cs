namespace CVMatrix.Maptrix.Util.Collections;

using System.Collections;

internal class MappedSetView<TOriginal, TMapped>(IReadOnlySet<TOriginal> original, Func<TOriginal, TMapped> mapFunction, Func<TMapped, TOriginal?> comparisonFunction)
    : IReadOnlySet<TMapped>
{
    public Func<TMapped, TOriginal?> ComparisonFunction { get; } = comparisonFunction;
    public Func<TOriginal, TMapped> MapFunction { get; } = mapFunction;
    public IReadOnlySet<TOriginal> OriginalSet { get; } = original;
    public int Count => OriginalSet.Count;

    public bool Contains(TMapped item)
    {
        return ComparisonFunction(item) is { } v && OriginalSet.Contains(v);
    }

    public IEnumerator<TMapped> GetEnumerator()
    {
        return OriginalSet.Select(MapFunction).GetEnumerator();
    }

    public bool IsProperSubsetOf(IEnumerable<TMapped> other)
    {
        return OriginalSet.IsProperSubsetOf(other.Select(ComparisonFunction).Where(x => x is not null)!);
    }

    public bool IsProperSupersetOf(IEnumerable<TMapped> other)
    {
        var otherList = other.Select(ComparisonFunction).ToList();
        return otherList.TrueForAll(x => x is not null) && OriginalSet.IsProperSupersetOf(otherList!);
    }

    public bool IsSubsetOf(IEnumerable<TMapped> other)
    {
        return OriginalSet.IsSubsetOf(other.Select(ComparisonFunction).Where(x => x is not null)!);
    }

    public bool IsSupersetOf(IEnumerable<TMapped> other)
    {
        var otherList = other.Select(ComparisonFunction).ToList();
        return otherList.TrueForAll(x => x is not null) && OriginalSet.IsSupersetOf(otherList!);
    }

    public bool Overlaps(IEnumerable<TMapped> other)
    {
        return OriginalSet.Overlaps(other.Select(ComparisonFunction).Where(x => x is not null)!);
    }

    public bool SetEquals(IEnumerable<TMapped> other)
    {
        var otherList = other.Select(ComparisonFunction).ToList();
        return otherList.TrueForAll(x => x is not null) && OriginalSet.SetEquals(otherList!);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}