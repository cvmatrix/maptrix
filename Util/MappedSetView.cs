namespace CVMatrix.DropOffDefense.SLib.Util;

using System.Collections;

internal class MappedSetView<TOriginal, TMapped>(IReadOnlySet<TOriginal> original, Func<TOriginal, TMapped> mapFunction, Func<TMapped, TOriginal> comparisonFunction)
    : IReadOnlySet<TMapped>
{
    public Func<TMapped, TOriginal> ComparisonFunction { get; } = comparisonFunction;
    public Func<TOriginal, TMapped> MapFunction { get; } = mapFunction;
    public IReadOnlySet<TOriginal> OriginalSet { get; } = original;
    public int Count => OriginalSet.Count;

    public bool Contains(TMapped item)
    {
        return OriginalSet.Contains(ComparisonFunction(item));
    }

    public IEnumerator<TMapped> GetEnumerator() => OriginalSet.Select(MapFunction).GetEnumerator());

    public bool IsProperSubsetOf(IEnumerable<TMapped> other)
    {
        return OriginalSet.IsProperSubsetOf(other.Select(ComparisonFunction));
    }

    public bool IsProperSupersetOf(IEnumerable<TMapped> other)
    {
        return OriginalSet.IsProperSupersetOf(other.Select(ComparisonFunction));
    }

    public bool IsSubsetOf(IEnumerable<TMapped> other)
    {
        return OriginalSet.IsSubsetOf(other.Select(ComparisonFunction));
    }

    public bool IsSupersetOf(IEnumerable<TMapped> other)
    {
        return OriginalSet.IsSupersetOf(other.Select(ComparisonFunction));
    }

    public bool Overlaps(IEnumerable<TMapped> other)
    {
        return OriginalSet.Overlaps(other.Select(ComparisonFunction));
    }

    public bool SetEquals(IEnumerable<TMapped> other)
    {
        return OriginalSet.Overlaps(other.Select(ComparisonFunction));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}