namespace CVMatrix.DropOffDefense.SLib.Util;

using System.Collections;

internal class MappedSetView<TOriginal, TMapped>(IReadOnlySet<TOriginal> original, Func<TOriginal, TMapped> mapFunction, Func<TMapped, TOriginal> comparisonFunction)
    : IReadOnlySet<TMapped>
{
    public IReadOnlySet<TOriginal> OriginalSet { get; } = original;
    public Func<TOriginal, TMapped> MapFunction { get; } = mapFunction;
    public Func<TMapped, TOriginal> ComparisonFunction { get; } = comparisonFunction;

    public IEnumerator<TMapped> GetEnumerator() => OriginalSet.Select(MapFunction).GetEnumerator());

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public int Count => OriginalSet.Count;
    public bool Contains(TMapped item) => OriginalSet.Contains(ComparisonFunction(item));

    public bool IsProperSubsetOf(IEnumerable<TMapped> other) => OriginalSet.IsProperSubsetOf(other.Select(ComparisonFunction));

    public bool IsProperSupersetOf(IEnumerable<TMapped> other) => OriginalSet.IsProperSupersetOf(other.Select(ComparisonFunction));

    public bool IsSubsetOf(IEnumerable<TMapped> other) => OriginalSet.IsSubsetOf(other.Select(ComparisonFunction));

    public bool IsSupersetOf(IEnumerable<TMapped> other) => OriginalSet.IsSupersetOf(other.Select(ComparisonFunction));

    public bool Overlaps(IEnumerable<TMapped> other) => OriginalSet.Overlaps(other.Select(ComparisonFunction));

    public bool SetEquals(IEnumerable<TMapped> other) => OriginalSet.Overlaps(other.Select(ComparisonFunction));
}