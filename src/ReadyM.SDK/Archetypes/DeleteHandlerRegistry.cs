using System.ComponentModel;
using ReadyM.Api.DI;
using ReadyM.SDK.Entities;

namespace ReadyM.SDK.Archetypes;

/// <summary>
/// Collects [DeleteHandler]-decorated handlers that run just before a shape goes. Unlike a create
/// handler, these run wherever the entity is: one that was replicated here is going away too, and
/// whatever was hung off it locally still has to be let go.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class DeleteHandlerRegistry
{
    private static readonly HandlerTable Table = new();

    public static IDependencyContainer Services => HandlerServices.Services;

    /// <summary>Called by generated code for a shape declaring a delete handler.</summary>
    public static void Register(ComponentSet shape, EntityHandler handler) => Table.Register(shape, handler);

    /// <summary>Called by generated code for a service watching a shape.</summary>
    public static void Observe(ComponentSet shape, EntityHandler handler) => Table.Observe(shape, handler);

    internal static bool Any => Table.Any;

    /// <summary>The mirror of the create order: a watcher goes first, and the shape's own handler last.</summary>
    internal static void RunAll(in EntityHandle handle) => Table.Run(handle, ownFirst: false);
}
