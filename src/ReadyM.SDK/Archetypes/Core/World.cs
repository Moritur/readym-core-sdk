using ReadyM.SDK.Attributes;

namespace ReadyM.SDK.Archetypes.Core;

/// <summary>The one entity that stands for the whole session and carries session-wide values.</summary>
[Archetype(replicated: true)]
public readonly partial struct World;