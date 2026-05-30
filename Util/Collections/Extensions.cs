namespace CVMatrix.DropOffDefense.SLib.Util.Collections;

internal static class Extensions
{
    public static MappedSetView<TOriginal, TMapped> MappedView<TOriginal, TMapped>(this IReadOnlySet<TOriginal> set, Func<TOriginal, TMapped> mapFunction, Func<TMapped, TOriginal> comparisonFunction)
    {
        return new(set, mapFunction, comparisonFunction);
    }
}