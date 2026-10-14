using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace ReadyM.Api.Generators.Archetypes;

/// <summary>A name a shape would expose twice: what it is called, and the two places it comes from.</summary>
internal sealed class NameClash(string name, string first, string second, Location location)
{
    public string Name { get; } = name;

    public string First { get; } = first;

    public string Second { get; } = second;

    public Location Location { get; } = location;
}

/// What a shape may include. [Include] composes mixins, as many as it likes. [IncludeArchetype] builds on an archetype,
/// which is inheritance, and the attribute itself allows one. Either way the shape exposes each name once.
internal static class IncludeRules
{
    public static readonly DiagnosticDescriptor Clash = new(
        "READYM041",
        "Two members of one name on a shape",
        "'{0}' would get '{1}' from both {2} and {3}. Give one of the includes a prefix, as in "
        + "[Include(typeof(X), \"Prefix\")], or rename one of the members.",
        "ReadyM",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor ArchetypeByInclude = new(
        "READYM042",
        "An archetype named by [Include]",
        "'{0}' includes the archetype '{1}' with [Include], which is for mixins. Build on it with "
        + "[IncludeArchetype(typeof({1}))] instead.",
        "ReadyM",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor MixinByIncludeArchetype = new(
        "READYM043",
        "A mixin named by [IncludeArchetype]",
        "'{0}' builds on '{1}' with [IncludeArchetype], but '{1}' is not an archetype. Include a mixin with "
        + "[Include(typeof({1}))] instead.",
        "ReadyM",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static ImmutableArray<Diagnostic> Check(DeclarationModel model)
    {
        var found = new List<Diagnostic>();

        foreach (var include in model.Includes)
        {
            var isArchetype = DeclarationModel.KindOf(include.Type) == IncludeKind.Archetype;

            if (!include.ByIncludeArchetype && isArchetype)
                found.Add(Diagnostic.Create(ArchetypeByInclude, include.Location, model.Name, include.Type.Name));
            else if (include.ByIncludeArchetype && !isArchetype)
                found.Add(Diagnostic.Create(MixinByIncludeArchetype, include.Location, model.Name, include.Type.Name));
        }

        // Names are only worth comparing once every include is the right kind: the shape has to change first
        if (found.Count == 0)
            found.AddRange(model.NameClashes().Select(clash
                => Diagnostic.Create(Clash, clash.Location, model.Name, clash.Name, clash.First, clash.Second)));

        return [.. found];
    }
}
