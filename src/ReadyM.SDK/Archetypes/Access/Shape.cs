using System.ComponentModel;
using ReadyM.Api.Mapping.Tags;

namespace ReadyM.SDK.Archetypes.Access;

/// <summary>Called by generated code.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class Shape
{
    public static Shape<TShape> Of<TShape, TComponent>()
        where TShape : struct, IArchetypeQueryable
        where TComponent : struct, IReadyComponent
        => new(new ShapeAccess<TComponent>());
}

/// <summary>Strongly typed wrapper around shape accessors.</summary>
public readonly struct Shape<TShape>
    where TShape : struct, IArchetypeQueryable
{
    internal ShapeAccess Access { get; }

    internal Shape(ShapeAccess access) => Access = access;
}