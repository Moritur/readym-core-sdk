using Friflo.Engine.ECS;

namespace ReadyM.Api.Saves;

/// <summary>
/// A component that writes and reads its own save record. Emitted by <c>[DeriveSavableComponent]</c>, or written by
/// hand for a component whose fields the generator does not cover.
/// </summary>
/// <exclude />
[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
public interface ISavableComponent : ISaveSerializable, IComponent
{
    string SaveName { get; }
    ushort SaveVersion { get; }
}
