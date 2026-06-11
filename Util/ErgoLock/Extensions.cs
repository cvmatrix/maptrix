namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

public static class Extensions
{
    public static T InlineReadScope<T>(this IErgoLock ergoLock, Func<T> func)
    {
        using var _ = ergoLock.ReadScope;
        return func();
    }

    public static void InlineReadScope<T>(this IErgoLock ergoLock, Action action)
    {
        using var _ = ergoLock.ReadScope;
        action();
    }

    public static T InlineUpgradeableScope<T>(this IErgoLock ergoLock, Func<IUpgradeableScope, T> func)
    {
        using var scope = ergoLock.UpgradeableScope;
        return func(scope);
    }

    public static void InlineUpgradeableScope<T>(this IErgoLock ergoLock, Action<IUpgradeableScope> action)
    {
        using var scope = ergoLock.UpgradeableScope;
        action(scope);
    }

    public static T InlineWriteScope<T>(this IErgoLock ergoLock, Func<T> func)
    {
        using var _ = ergoLock.WriteScope;
        return func();
    }

    public static void InlineWriteScope<T>(this IErgoLock ergoLock, Action action)
    {
        using var _ = ergoLock.WriteScope;
        action();
    }
}