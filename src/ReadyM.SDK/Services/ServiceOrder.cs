using System.ComponentModel;

namespace ReadyM.SDK.Services;

/// <summary>Where a service's update sits relative to the others.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public readonly struct ServiceOrder(int priority, Type[]? before, Type[]? after)
{
    public int Priority { get; } = priority;

    public Type[] Before { get; } = before ?? [];

    public Type[] After { get; } = after ?? [];
}
