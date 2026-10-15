using ReadyM.Api.Idents;
using ReadyM.Api.Multiplayer.ECS.Components;
using ReadyM.SDK.Attributes;

namespace ReadyM.SDK.Archetypes.Core;

/// A scope for one connected player, from connecting until disconnecting.
[Archetype(replicated: true)]
[ExplicitComponent(typeof(PlayerScopeComponent))]
public readonly partial struct Player : IScope
{
    /// Which player this is.
    public partial PlayerId PlayerId { get; set; }
}