using LiteNetLib.Utils;

namespace ReadyM.Api.Serialization;

/// Variable-length unsigned integer.
/// Most significant bit of each byte is used as a continuation flag.
/// The remaining 7 bits are used to store the value.
public struct VarUInt(uint value) : INetSerializable
{
    public const int MaxSize = 5;

    public uint Value { get; private set; } = value;

    /// How many bytes the value takes on the wire.
    public static int SizeOf(uint value)
        => value switch
        {
            < 1u << 7 => 1,
            < 1u << 14 => 2,
            < 1u << 21 => 3,
            < 1u << 28 => 4,
            _ => 5,
        };

    public void Serialize(NetDataWriter writer)
        => writer.PutVarUInt(Value);

    public void Deserialize(NetDataReader reader)
        => Value = reader.GetVarUInt();

    public static implicit operator uint(VarUInt value)
        => value.Value;

    public static implicit operator VarUInt(uint value)
        => new(value);
}
