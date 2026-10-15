using ReadyM.SDK.Attributes;

namespace ReadyM.SDK.Tests.Client.Fixtures;

/// <summary>A role a host looks services up by, the way the server finds mod seeders.</summary>
public interface IGreeter
{
    string Greet();
}

/// <summary>Declared after its sibling in the file but named before it, so the order is the name's.</summary>
[Service]
public sealed partial class ZuluGreeter : IGreeter
{
    public string Greet() => "zulu";
}

[Service]
public sealed partial class AlphaGreeter : IGreeter
{
    public string Greet() => "alpha";
}
