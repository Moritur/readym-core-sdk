using ReadyM.SDK.Entities;

namespace ReadyM.SDK.Archetypes.Access;

/// Reads one of an entity's collections, as its component exposes it.
public delegate TAccess CollectionAccess<out TAccess>(in EntityHandle handle)
#if NET
    where TAccess : allows ref struct
#endif
;