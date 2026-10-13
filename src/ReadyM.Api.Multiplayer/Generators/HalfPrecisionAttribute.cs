using System;

namespace ReadyM.Api.Multiplayer.Generators;

/// The annotated float or vector will be serialized on the wire as 16-bit floats, saving 2 bytes per float.
/// <remarks>
/// Keeps about three significant digits and reaches up to 65504, so it suits values with no fixed range, like speeds,
/// velocities and play rates. Do not use it for world positions: far from the origin a step becomes tens of units.
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class HalfPrecisionAttribute : Attribute;
