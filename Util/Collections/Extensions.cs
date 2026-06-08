namespace CVMatrix.DropOffDefense.SLib.Util.Collections;

internal static class Extensions
{
    public static MappedSetView<TOriginal, TMapped> MappedView<TOriginal, TMapped>(this IReadOnlySet<TOriginal> set, Func<TOriginal, TMapped> mapFunction, Func<TMapped, TOriginal?> comparisonFunction)
    {
        return new(set, mapFunction, comparisonFunction);
    }

    public static MappedSetView<TOriginal, TMapped> CastingView<TOriginal, TMapped>(this IReadOnlySet<TOriginal> set)
        where TOriginal : TMapped => MappedView(set, x => (TMapped)x, x => x is TOriginal v ? v : default);
}