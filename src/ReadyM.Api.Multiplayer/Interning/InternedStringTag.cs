namespace ReadyM.Api.Multiplayer.Interning;

/// Wire representation of an interned string.
public enum InternedStringTag : byte
{
    Inline = 0,
    Id = 1,
}
