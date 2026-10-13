using System.Collections.Generic;
using System.IO;
using LiteNetLib.Utils;
using Yooni.Native.Container;
using Yooni.Native.Serialization;

namespace ReadyM.Api.Multiplayer.Interning;

/// A client's copy of the server's index, filled only from what the server sends.
public sealed class ClientInternedStringIndex : IInternedStringIndex
{
    private readonly Dictionary<NativeString256, uint> _ids = new();
    private readonly List<NativeString256> _names = new();

    public int Count => _names.Count;

    public bool TryGetIdForWrite(in NativeString256 value, out uint id)
        => _ids.TryGetValue(value, out id);

    public bool TryResolve(uint id, out NativeString256 value)
    {
        if (id < (uint)_names.Count)
        {
            value = _names[(int)id];
            return true;
        }

        value = default;
        return false;
    }

    /// Adds the strings from an append or a whole index.
    public void ApplyAppend(NetDataReader reader)
    {
        var firstId = reader.GetUInt();
        var count = reader.GetInt();

        // Appends arrive in order and a whole index only follows Clear, so a gap or an overlap means the copies diverged
        if (firstId != (uint)_names.Count)
            throw new InvalidDataException($"An interned string append starts at id {firstId}, but this index has {_names.Count}");
        if (count < 0)
            throw new InvalidDataException($"An interned string append has a negative count {count}");

        for (var i = 0; i < count; i++)
        {
            var name = default(NativeString256);
            name.Deserialize(reader);

            if (_ids.ContainsKey(name))
                throw new InvalidDataException($"An interned string append repeats \"{name}\"");

            _ids.Add(name, (uint)_names.Count);
            _names.Add(name);
        }
    }

    public void Clear()
    {
        _ids.Clear();
        _names.Clear();
    }
}
