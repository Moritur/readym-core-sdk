using System;

namespace ReadyM.Api.Multiplayer.Generators;

/// <summary>
/// The float or vector field is sent over the wire as a fixed-point number in <c>bits</c> bits (8, 16 or 24) between <c>min</c> and
/// <c>max</c>, instead of a 4-byte float per number.
/// </summary>
/// <remarks>
/// Values outside the range are clamped, so pick a range the value can never leave. The step between two values the
/// network can tell apart is <c>(max - min) / (2^bits - 1)</c>; a change smaller than that is not sent.
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class QuantizeAttribute(float min, float max, int bits) : Attribute
{
    public float Min { get; } = min;
    public float Max { get; } = max;
    public int Bits { get; } = bits;
}
