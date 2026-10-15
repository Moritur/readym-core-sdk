namespace ReadyM.SDK.Attributes;

/// <summary>How this shape's values travel between the game and the ECS, and who may write them.</summary>
[AttributeUsage(AttributeTargets.Struct)]
public sealed class PropagatesAttribute(Propagation propagation) : Attribute
{
    public Propagation Propagation { get; } = propagation;
}
