namespace CVMatrix.DropOffDefense.SLib.Util;

internal static class Extensions
{
    public static MappedSetView<TOriginal, TMapped> MappedView<TOriginal, TMapped>(this IReadOnlySet<TOriginal> set, Func<TOriginal, TMapped> mapFunction, Func<TMapped, TOriginal> comparisonFunction) => new(set, mapFunction, comparisonFunction);
}