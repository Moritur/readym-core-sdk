using System;

namespace ReadyM.Api.Multiplayer.Generators;

/// <summary>
/// Indicates a float field holding an angle in degrees as <c>bits</c> bits (8 or 16), splitting the full circle into
/// 2^bits steps.
/// </summary>
/// <remarks>
/// The angle wraps around the circle: 190 arrives as -170. The receiver always gets a value in [-180, 180). With 16
/// bits a step is about 0.0055 degrees, with 8 bits about 1.4 degrees.
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class QuantizeAngleAttribute(int bits) : Attribute
{
    public int Bits { get; } = bits;
}
