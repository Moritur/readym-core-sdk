namespace ReadyM.SDK.Client.Mapping;

/// A client mod's game-state mappings, declared once when the mod loads.
public interface IShapeMappings
{
    /// Declares the mappings on <paramref name="registry"/>.
    void Register(IShapeMappingRegistry registry);
}