using System;
using LiteNetLib.Utils;
using ReadyM.Api.Serialization;
using Yooni.Native.Container;
using Yooni.Native.Serialization;

namespace ReadyM.Api.Idents;

/// Identifies an area: a part of the game world behind a loading screen, such as a whole map or a single interior.
[DeriveJsonSerializable]
public partial struct AreaId : INetSerializable, IEquatable<AreaId>
{
    private NativeString256 _id;

    /// An area id from its native string.
    public AreaId(NativeString256 id)
    {
        _id = id;
    }
    
    /// An area id from its string form.
    public AreaId(string id)
    {
        _id = new NativeString256(id, true);
    }
    
    /// An area id from a number, stored as its decimal text.
    public AreaId(int id)
    {
        _id = new NativeString256(id.ToString(), true);
    }

    /// No area.
    public static AreaId Invalid => default;

    internal string RawValue => _id.ToString();

    public string ToManaged()
        => _id.ToManaged();

    public void Serialize(NetDataWriter writer)
    {
        _id.Serialize(writer);
    }

    public void Deserialize(NetDataReader reader)
    {
        _id.Deserialize(reader);
    }

    /// Whether both ids name the same area.
    public bool Equals(AreaId other)
        => _id == other._id;

    public override bool Equals(object? obj)
        => obj is AreaId other && Equals(other);

    public override int GetHashCode()
        => _id.GetHashCode();

    /// Whether both ids name the same area.
    public static bool operator ==(AreaId left, AreaId right)
        => left._id == right._id;

    /// Whether the ids name different areas.
    public static bool operator !=(AreaId left, AreaId right)
        => left._id != right._id;

    public override string ToString()
        => _id == Invalid._id ? "AreaId.Invalid" : $"AreaId[{_id}]";
}