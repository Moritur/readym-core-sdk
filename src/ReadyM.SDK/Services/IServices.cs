namespace ReadyM.SDK.Services;

/// <summary>Switches services on and off while the game runs.</summary>
public interface IServices
{
    /// <summary>Lets its update run again and calls its OnEnabled. Warns and does nothing if already enabled.</summary>
    void Enable<TService>() where TService : class, IService;

    /// <summary>
    /// Stops its update running and calls its OnDisabled. Warns and does nothing if already disabled.
    /// </summary>
    void Disable<TService>() where TService : class, IService;

    bool IsEnabled<TService>() where TService : class, IService;
}
