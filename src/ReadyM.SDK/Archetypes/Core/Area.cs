using ReadyM.Api.Idents;
using ReadyM.Api.Multiplayer.ECS.Components;
using ReadyM.SDK.Attributes;

namespace ReadyM.SDK.Archetypes.Core;

/// A scope for one area of the world, holding what is in it while players are there.
[Archetype(replicated: true)]
[ExplicitComponent(typeof(AreaScopeComponent))]
public readonly partial struct Area : IScope
{
    /// Which area this is.
    [Index]
    public partial AreaId AreaId { get; set; }
    /// The player whose game simulates what the area holds.
    public partial PlayerId MasterClient { get; set; }
}