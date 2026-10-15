namespace ReadyM.SDK.Attributes;

/// <summary>Runs this method on an entity right after it is created.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class CreateHandlerAttribute : Attribute
{
    public CreateHandlerAttribute() { }

    public CreateHandlerAttribute(Type shape) => Shape = shape;

    public Type? Shape { get; }
}
