using System.ComponentModel;

namespace ReadyM.SDK.Services;

/// <summary>
/// What a [Service] declaring a Start or an OnEnabled compiles down to.
/// Implemented by generated code.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IStartedService
{
    /// <summary>Runs the service's own Start and its first OnEnabled.</summary>
    void Start();
}
