using Friflo.Engine.ECS;

namespace ReadyM.Api.Mapping.Tags;

/// <summary>
/// Marks a component only the server is allowed to modify.
/// </summary>
/// <exclude />
[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
public interface IServerAuthoritative : IMappingContext<Entity>
{
    // empty
}
