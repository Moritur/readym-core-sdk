using LiteNetLib;
using ReadyM.Api.Idents;
using ReadyM.Relay.Server.Sdk.Ecs;

namespace ReadyM.Relay.Server.Sdk.Events;

/// <summary>The server's events: players connecting and moving, and areas and cells coming and going.</summary>
public interface IServerEvents
{
    /// <summary>Fires when a player has connected and their player entity exists.</summary>
    event Action<PlayerId, Entity>? OnPlayerConnected;

    /// <summary>Fires when a player has left or dropped, after they left their area.</summary>
    event Action<PlayerId, Entity, DisconnectReason>? OnPlayerDisconnected;

    /// <summary>Fires when an area comes into use, as the first player enters it.</summary>
    event Action<AreaId, Entity>? OnAreaCreated;

    /// <summary>Fires when the last player has left an area, unless the server keeps it while empty.</summary>
    event Action<AreaId, Entity>? OnAreaDeleted;

    /// <summary>Fires when any player enters an area, the first one included.</summary>
    event Action<PlayerId, AreaId>? OnPlayerJoinedArea;

    /// <summary>Fires when any player leaves an area.</summary>
    event Action<PlayerId, AreaId>? OnPlayerLeftArea;

    /// <summary>Fires when a cell comes into use, as the first player loads it.</summary>
    event Action<AreaId, CellId, Entity>? OnCellCreated;

    /// <summary>Fires when the last player has unloaded a cell, unless the server keeps it while empty.</summary>
    event Action<AreaId, CellId, Entity>? OnCellDeleted;

    /// <summary>Fires when any player loads a cell, the first one included.</summary>
    event Action<PlayerId, AreaId, CellId>? OnPlayerActivatedCell;

    /// <summary>Fires when any player unloads a cell.</summary>
    event Action<PlayerId, AreaId, CellId>? OnPlayerDeactivatedCell;

    /// <summary>
    /// Raised once, when the world entity exists. A mod's Init runs before that, so this is the earliest point at which
    /// world components can be written.
    /// </summary>
    event Action<Entity>? OnWorldEntityCreated;
}
