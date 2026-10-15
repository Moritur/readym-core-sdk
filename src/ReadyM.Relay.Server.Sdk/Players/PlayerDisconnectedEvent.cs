using ReadyM.Api.Idents;

namespace ReadyM.Relay.Server.Sdk.Players;

[Obsolete("Use IServerEvents instead")]
public sealed class PlayerDisconnectedEvent
{
    public required PlayerId PlayerId { get; init; }
    public required Guid ReadyMId { get; init; }
}
