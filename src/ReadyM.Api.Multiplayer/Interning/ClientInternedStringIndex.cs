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

    /// Adds the strings from an append, or replaces the copy with a whole index, which is the only append starting at 0.
    public void ApplyAppend(NetDataReader reader)
    {
        var firstId = reader.GetUInt();
        var count = reader.GetInt();

        if (firstId == 0)
            Clear();

        // Appends arrive in order, so a gap means the copies diverged. An overlap is a reconnect into a live session
        // repeating what the whole index just gave, and has to match it.
        if (firstId > (uint)_names.Count)
            throw new InvalidDataException($"An interned string append starts at id {firstId}, but this index has {_names.Count}");
        if (count < 0)
            throw new InvalidDataException($"An interned string append has a negative count {count}");

        for (var i = 0; i < count; i++)
        {
            var name = default(NativeString256);
            name.Deserialize(reader);

            var id = firstId + (uint)i;
            if (id < (uint)_names.Count)
            {
                if (_names[(int)id] != name)
                    throw new InvalidDataException($"Interned string {id} is \"{_names[(int)id]}\" here, but an append says \"{name}\"");

                continue;
            }

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
