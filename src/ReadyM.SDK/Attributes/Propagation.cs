namespace ReadyM.SDK.Attributes;

/// <summary>Which way a shape's values may travel between the game and the ECS, and who may write them.</summary>
public enum Propagation
{
    /// <summary>The game owns it. It is read into the ECS and replicated, and nothing writes it back.</summary>
    /// <example>A flag that indicates whether an enemy is a boss.</example>
    ToEcsOnly,

    /// <summary>The ECS owns it. It is shown to the game, and the game is never read for it.</summary>
    ToGameOnly,

    /// <summary>Both ways, and anyone may set or override it.</summary>
    /// <example>Any data you want to make settable by anyone.</example>
    Both,

    /// <summary>Only the server sets it. A client reads it and applies it to the game state.</summary>
    /// <remarks>Use it for server-side configuration replicated to players, or data that cannot be trusted to be sent by the clients (e.g. for cheating prevention).</remarks>
    /// <example>Custom rule flags that prohibit using certain abilities.</example>
    ServerAuthoritative,

    /// <summary>Only whoever owns the entity may report or override it. The rest read it.</summary>
    /// <example>Player HP, inventory, etc. Use in mods that trust the clients.</example>
    OwnershipBased
}