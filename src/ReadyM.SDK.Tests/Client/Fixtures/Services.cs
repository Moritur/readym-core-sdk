using ReadyM.SDK.Attributes;
using ReadyM.SDK.Client.Entities;
using ReadyM.SDK.Services;

namespace ReadyM.SDK.Tests.Client.Fixtures;

/// <summary>A service asks for what it needs in its constructor, the way any class does.</summary>
[Service]
public sealed partial class Regeneration(IEntities entities)
{
    public float Healed { get; private set; }

    private void Update()
    {
        foreach (var chest in entities.Query<Chest>())
            chest.Gold += (int)(5f * Time.DeltaTime);

        Healed += Time.DeltaTime;
    }
}

/// <summary>An update takes nothing: a service that cares about time reads it off its own Time.</summary>
[Service]
public sealed partial class Counting
{
    public int Ticks { get; private set; }

    private void Update() => Ticks++;
}

/// <summary>Reads the clock off itself, so a test can see what a game put together.</summary>
[Service]
public sealed partial class Recording
{
    public UpdateTime Last { get; private set; }

    private void Update() => Last = Time;
}

/// <summary>A service is a singleton like any other, so one can be handed another.</summary>
[Service]
public sealed partial class Auditing(Counting counting)
{
    public int Seen { get; private set; }

    private void Update() => Seen = counting.Ticks;
}

/// <summary>
/// Nothing runs on it and nothing watches through it. It is still one instance anything can ask for,
/// which is what most services are for.
/// </summary>
[Service]
public sealed partial class Settings
{
    public int Difficulty { get; set; } = 3;
}

/// <summary>Start and Stop are the game's own bookends, and a service may declare either one alone.</summary>
[Service]
public sealed partial class Bookends
{
    public int Started { get; private set; }

    public int Stopped { get; private set; }

    private void Start() => Started++;

    private void Stop() => Stopped++;
}

[Service]
public sealed partial class StartOnly
{
    public bool Started { get; private set; }

    private void Start() => Started = true;
}

/// <summary>Watches a shape it did not declare, which is how a service reaches a moment a mixin owns.</summary>
[Service]
public sealed partial class Bookkeeping
{
    public List<int> Seen { get; } = [];

    [CreateHandler(typeof(Parcel))]
    private void Track(Parcel parcel) => Seen.Add(parcel.Mark);
}

/// <summary>Both ends of the switch, so a test can see when each one ran and what ticked in between.</summary>
[Service]
public sealed partial class Lanterns
{
    public int Ticks { get; private set; }

    public int Enables { get; private set; }

    public int Disables { get; private set; }

    private void Update() => Ticks++;

    private void OnEnabled() => Enables++;

    private void OnDisabled() => Disables++;
}

/// <summary>The order a service is set up in: Start first, then being told it is about to run.</summary>
[Service]
public sealed partial class Opening
{
    public List<string> Order { get; } = [];

    private void Start() => Order.Add("Start");

    private void OnEnabled() => Order.Add("OnEnabled");
}

/// <summary>
/// Enabled is read-only, so a service that wants to stop itself goes through the same door anything
/// else does, and its OnDisabled runs as it would for anyone.
/// </summary>
[Service]
public sealed partial class Curfew(IServices services)
{
    public int Ticks { get; private set; }

    public int Disables { get; private set; }

    private void Update()
    {
        Ticks++;
        services.Disable<Curfew>();
    }

    private void OnDisabled() => Disables++;
}
