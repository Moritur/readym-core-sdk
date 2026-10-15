namespace ReadyM.Relay.Server.Sdk.Interop;

/// <exclude/>
public struct ModPointers
{
    public IntPtr TickSystems;

    /// <summary>Invoked by the host after it creates an entity, so mod components can run their native init.</summary>
    public IntPtr PostCreateEntityInit;

    /// <summary>Invoked by the host before it deletes an entity, so mod shapes can run their delete handlers.</summary>
    public IntPtr EntityDeleted;

    /// <summary>
    /// <c>SeederDescriptor[SeederCount]</c>, in the order the server registers them. Mod host memory, never freed.
    /// </summary>
    public IntPtr Seeders;

    public int SeederCount;

    /// <summary><c>SeedScopeDelegate</c> over the indexes of <see cref="Seeders"/>; zero when there are none.</summary>
    public IntPtr SeedScope;
}