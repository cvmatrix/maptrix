namespace CVMatrix.DropOffDefense.SLib.Core.Internal;

using Behaviors;
using Handles;
internal abstract class SimObjectHandle<TSource, THandle, TBehavior, TStats, TMessage, TAction>(TSource source) : IHandle<TMessage, TStats> where TSource : SimObject<THandle, TBehavior, TStats, TMessage, TAction> where TBehavior : IBehavior<THandle, TMessage, TAction>
{
    public TSource Source { get; } = source;
    public TStats Stats => Source.Stats;
    public void EnsureUpdated() => Source.Tick();
    public void SendMessage(TMessage message) => Source.RecieveActions(Source.Behavior.OnRecieveMessage(message));
    public override bool Equals(object? obj) => obj is SimObjectHandle<TSource, THandle, TBehavior, TStats, TMessage, TAction> other && Source.Equals(other.Source);
    public override int GetHashCode() => Source.GetHashCode();
}