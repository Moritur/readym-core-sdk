using System.ComponentModel;
using ReadyM.Api.DI;
using ReadyM.SDK.Entities;

namespace ReadyM.SDK.Archetypes;

/// <summary>Collects [CreateHandler]-decorated handlers that run as a shape appears.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class CreateHandlerRegistry
{
    private static readonly HandlerTable Table = new();

    /// <summary>Call this once when the game initializes to wire up the DI for the handlers.</summary>
    public static void Use(IDependencyContainer services) => HandlerServices.Use(services);

    public static IDependencyContainer Services => HandlerServices.Services;

    /// <summary>Called by generated code for a shape declaring a create handler.</summary>
    public static void Register(ComponentSet shape, EntityHandler handler) => Table.Register(shape, handler);

    /// <summary>Called by generated code for a service watching a shape.</summary>
    public static void Observe(ComponentSet shape, EntityHandler handler) => Table.Observe(shape, handler);

    internal static bool Any => Table.Any;

    /// <summary>The shape's own handler first, so a watcher reads what the shape set up for itself.</summary>
    internal static void RunAll(in EntityHandle handle) => Table.Run(handle, ownFirst: true);
}
