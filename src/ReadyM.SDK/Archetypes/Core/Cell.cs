using ReadyM.Api.Idents;
using ReadyM.Api.Multiplayer.ECS.Components;
using ReadyM.SDK.Attributes;

namespace ReadyM.SDK.Archetypes.Core;

/// A scope for one loaded cell of an area, holding what is in it.
[Archetype(replicated: true)]
[ExplicitComponent(typeof(CellScopeComponent))]
public readonly partial struct Cell : IScope
{
    /// Which cell this is, with its area.
    [Index]
    public partial FullCellId FullCellId { get; set; }

    /// The player whose game simulates what the cell holds, or <c>PlayerId.Invalid</c> while nobody is there.
    public partial PlayerId MasterClient { get; set; }
}
