namespace ReadyM.Relay.Server.Sdk.Interop;

/// <exclude/>
public struct ArchetypePointers
{
    public required IntPtr RegisterArchetype;
    public required IntPtr ModifyArchetype;
    public required IntPtr ResolveArchetype;

    /// <summary>Puts one tag on an archetype, named by its full type name.</summary>
    public required IntPtr AddArchetypeTag;
}