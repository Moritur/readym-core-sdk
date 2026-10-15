using Microsoft.CodeAnalysis.Diagnostics;

namespace ReadyM.Api.Generators.Archetypes;

/// <summary>
/// Which half of the SDK a compilation is for. A client never writes a value straight into a chunk:
/// that path skips the component's policy and its masks, which is the whole of what a client write
/// has to obey. A server writes what it likes, so it keeps them.
/// </summary>
internal static class SdkRuntime
{
    private const string Property = "build_property.SdkRuntime";

    /// <summary>Defaults to the server, so a build that says nothing keeps everything it had.</summary>
    public static bool IsClient(AnalyzerConfigOptionsProvider options)
        => options.GlobalOptions.TryGetValue(Property, out var side)
           && side.Equals("Client", System.StringComparison.OrdinalIgnoreCase);
}
