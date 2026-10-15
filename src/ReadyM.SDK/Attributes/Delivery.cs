namespace ReadyM.SDK.Attributes;

/// <summary>How a replicated shape's changes reach the other side.</summary>
public enum Delivery
{
    /// <summary>Guaranteed to arrive, in the order it was sent.</summary>
    Reliable,

    /// <summary>
    /// Sent once and never resent. Useful for a value written often enough that losing one is corrected by
    /// the next write, such as a position.
    /// </summary>
    Unreliable
}
