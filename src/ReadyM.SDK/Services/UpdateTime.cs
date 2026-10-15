namespace ReadyM.SDK.Services;

/// <summary>A service's clock for the current update.</summary>
/// <param name="deltaTime">Seconds since the last update.</param>
/// <param name="time">Seconds since the game started.</param>
/// <param name="ticks">Updates that came before this one.</param>
public readonly struct UpdateTime(float deltaTime, float time, ulong ticks = 0)
{
    /// <summary>Seconds since the last update.</summary>
    public float DeltaTime { get; } = deltaTime;

    /// <summary>Seconds since the game started.</summary>
    public float Elapsed { get; } = time;

    /// <summary>How many updates came before this one.</summary>
    public ulong Ticks { get; } = ticks;

    public override string ToString() => $"update {Ticks}: {DeltaTime}s of {Elapsed}s";
}
