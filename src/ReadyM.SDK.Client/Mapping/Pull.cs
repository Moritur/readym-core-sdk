namespace ReadyM.SDK.Client.Mapping;

/// <summary>Reads a value out of the game and into the shape.</summary>
public delegate void Pull<TValue, in TContext>(ref TValue value, TContext context);