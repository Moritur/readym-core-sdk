namespace ReadyM.SDK.Attributes;

/// <summary>Runs this method on an entity just before it is deleted.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class DeleteHandlerAttribute : Attribute
{
    public DeleteHandlerAttribute() { }

    public DeleteHandlerAttribute(Type shape) => Shape = shape;

    /// <summary>The shape watched from a service. Null on a shape's own handler, which watches itself.</summary>
    public Type? Shape { get; }
}
