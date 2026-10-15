using System.Numerics;
using LiteNetLib.Utils;
using ReadyM.Api.Idents;
using ReadyM.Api.Multiplayer.Serialization;

namespace ReadyM.SDK.Tests.Rpc;

/// <summary>What an RPC parameter goes through when it is not a primitive or a serializable struct.</summary>
public class RelaySerializerTests
{
    private readonly RelaySerializer _serializer = new([new DefaultRelaySerializerRegistration()]);

    /// <summary>One value of every type the default registration covers.</summary>
    public static TheoryData<object> Values => new()
    {
        new PlayerId(7),
        (byte)200,
        (short)-1234,
        (ushort)54321,
        -123456,
        -1234567890123L,
        1.5f,
        -2.25,
        "hello",
        true,
        new byte[] { 1, 2, 255 },
        new[] { -1, 0, int.MaxValue },
        new Vector2(1.5f, -2f),
        new Vector3(1.5f, -2f, 3.25f),
        new Vector4(1.5f, -2f, 3.25f, 100f),
        new[] { true, false, true },
        new short[] { -1, 0, short.MaxValue },
        new ushort[] { 0, 1, ushort.MaxValue },
        new uint[] { 0, 1, uint.MaxValue },
        new[] { -1L, 0L, long.MaxValue },
        new ulong[] { 0, 1, ulong.MaxValue },
        new[] { -1.5f, 0f, float.MaxValue },
        new[] { -1.5, 0.0, double.MaxValue },
        new[] { "a", "", "ünïcode" },
    };

    [Theory]
    [MemberData(nameof(Values))]
    public void A_value_comes_back_as_it_was_sent(object value)
    {
        var read = RoundTrip(value);

        Assert.IsType(value.GetType(), read);
        Assert.Equal(value, read);
    }

    /// <summary>An empty array still says which array it was.</summary>
    [Theory]
    [MemberData(nameof(EmptyArrays))]
    public void An_empty_array_comes_back_empty(object value)
    {
        var read = RoundTrip(value);

        Assert.IsType(value.GetType(), read);
        Assert.Empty((System.Collections.IEnumerable)read!);
    }

    public static TheoryData<object> EmptyArrays => new()
    {
        Array.Empty<byte>(),
        Array.Empty<int>(),
        Array.Empty<bool>(),
        Array.Empty<short>(),
        Array.Empty<ushort>(),
        Array.Empty<uint>(),
        Array.Empty<long>(),
        Array.Empty<ulong>(),
        Array.Empty<float>(),
        Array.Empty<double>(),
        Array.Empty<string>(),
    };

    private object? RoundTrip(object value)
    {
        var writer = new NetDataWriter();
        _serializer.SerializeObject(writer, value);

        return _serializer.DeserializeObject(new NetDataReader(writer));
    }
}
