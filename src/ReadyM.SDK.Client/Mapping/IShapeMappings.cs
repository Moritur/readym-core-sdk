namespace ReadyM.SDK.Client.Mapping;

/// <summary>A client mod's game-state mappings, declared once when the mod loads.</summary>
public interface IShapeMappings
{
    /// <summary>Declares the mappings on <paramref name="registry"/>.</summary>
    void Register(IShapeMappingRegistry registry);
}