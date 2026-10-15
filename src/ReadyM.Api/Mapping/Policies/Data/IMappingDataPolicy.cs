namespace ReadyM.Api.Mapping.Policies.Data;

internal interface IMappingDataPolicy<TContext> : IMappingDataPolicyBase
{
    /// <summary>Should data be copied from the game object to the ECS entity</summary>
    bool ShouldGameCopyToEcs(in TContext context);

    /// <summary>Should data be copied from the ECS entity to the game object</summary>
    bool ShouldEcsCopyToGame(in TContext context);
    
    /// <summary>Should data be allowed to be set from the API.</summary>
    bool CanSetFromApi(in TContext context);

    /// <summary>Should data be allowed to be stored / set locally (analogous to ShouldRunLocally for events).</summary>
    bool CanGameSetLocally(in TContext context);
}