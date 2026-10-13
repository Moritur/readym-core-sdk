using System.IO;
using LiteNetLib.Utils;
using Yooni.Native.Container;
using Yooni.Native.Serialization;

namespace ReadyM.Api.Multiplayer.Interning;

/// Writes and reads a string marked [Interned]. Called by generated code.
public static class InternedStringCodec
{
    public static void Write(NetDataWriter writer, in NativeString256 value)
    {
        if (InternedStrings.Current is { } index && index.TryGetIdForWrite(value, out var id))
        {
            writer.Put((byte)InternedStringTag.Id);
            writer.Put(id);
            return;
        }

        writer.Put((byte)InternedStringTag.Inline);
        value.Serialize(writer);
    }

    public static void Read(NetDataReader reader, ref NativeString256 value)
    {
        var tag = (InternedStringTag)reader.GetByte();
        switch (tag)
        {
            case InternedStringTag.Inline:
                value.Deserialize(reader);
                return;

            case InternedStringTag.Id:
                var id = reader.GetUInt();
                // An id the reader cannot resolve is still fixed width, so the rest of the message reads correctly
                if (InternedStrings.Current is not { } index)
                {
                    // Nothing to report through; registration keeps marked fields out of the plugin host, the one reader without an index
                    value = default;
                }
                else if (index.TryResolve(id, out var name))
                {
                    value = name;
                }
                else
                {
                    value = default;
                    index.ReportUnresolved(id);
                }

                return;

            default:
                throw new InvalidDataException($"Unknown interned string tag {(byte)tag}");
        }
    }
}
