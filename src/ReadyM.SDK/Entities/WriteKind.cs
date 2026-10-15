namespace ReadyM.SDK.Entities;

/// <summary>Component field write kind - either mirroring the game state or overriding it.</summary>
public enum WriteKind : byte
{
    /// <summary>
    /// Indicates the set value is a mirror of the game state, and should be replicated to other players.
    /// </summary>
    Mirror,

    /// <summary>
    /// Indicates an override of the state, which should be applied to the entity's owner's game state, and replicated to other players.
    /// Can only be used on components that have a policy that allows it.
    /// </summary>
    Override
}