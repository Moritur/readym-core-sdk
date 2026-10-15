using ReadyM.SDK.Archetypes;

namespace ReadyM.SDK.Client.Mapping;

/// <summary>Where a mod declares how its shapes map to and from game objects.</summary>
public interface IShapeMappingRegistry
{
    /// <summary>Everything this shape maps to and from one kind of game object.</summary>
    IShapeMappingScope<TShape, TContext> For<TShape, TContext>()
        where TShape : struct, IArchetypeQueryable;
}