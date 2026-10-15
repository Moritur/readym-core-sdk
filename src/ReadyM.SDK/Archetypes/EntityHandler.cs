using System.ComponentModel;
using ReadyM.SDK.Entities;

namespace ReadyM.SDK.Archetypes;

/// <summary>What a shape or a service asked to run at a moment in an entity's life.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public delegate void EntityHandler(in EntityHandle handle);
