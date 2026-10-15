using System.ComponentModel;

namespace ReadyM.SDK.Services;

/// <summary>What every [Service] compiles down to. Implemented by generated code.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IService
{
    ServiceSwitch Switch { get; }

    /// <summary>Runs the service's own OnEnabled or OnDisabled, once the switch has moved.</summary>
    void Switched(bool enabled);
}
