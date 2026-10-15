using System.ComponentModel;

namespace ReadyM.SDK.Services;

/// <summary>What a [Service] declaring an update compiles down to. Implemented by generated code.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IUpdatingService : IService
{
    void Update(in UpdateTime time);
}
