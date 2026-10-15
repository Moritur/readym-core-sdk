using ReadyM.SDK.Archetypes;
using ReadyM.SDK.Archetypes.Access;
using ReadyM.SDK.Entities;

namespace ReadyM.SDK.Client;

/// Changes a client makes to replicated values it does not set directly.
public static class WriteExtensions
{
    /// Sets a replicated value from this client; false, and nothing changes, when this client may not set it.
    public static bool Override<TShape, TOwner, TValue>(this TShape shape, Value<TOwner, TValue> value, TValue set)
        where TShape : struct, IArchetypeQueryable
        where TOwner : struct, IArchetypeQueryable
        => value.Access.Write(EntityHandle.Of(shape), set, WriteKind.Override);
}
