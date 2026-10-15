namespace ReadyM.SDK.Client.Mapping;

/// <summary>Moves one collection between the ECS and the game, in whichever direction it was declared for.</summary>
public delegate void Collected<TAccess, in TContext>(TAccess collection, TContext context)
#if NET
    where TAccess : allows ref struct
#endif
    ;
