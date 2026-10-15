using ReadyM.SDK.Attributes;

namespace ReadyM.SDK.Archetypes;

/// <summary>One component that replicates, and how its changes travel.</summary>
internal readonly struct ReplicatedComponent(Type component, Delivery delivery)
{
    public Type Component { get; } = component;

    public Delivery Delivery { get; } = delivery;
}
