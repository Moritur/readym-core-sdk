using System.Collections.Generic;
using System.Linq;

namespace ReadyM.Api.Generators.Archetypes;

/// <summary>Emits the wrapper a mapping handler is handed for one collection.</summary>
internal static class CollectionAccessEmitter
{
    public static string NameOf(DeclarationModel model, string collection) => model.Name + collection;

    public static string QualifiedNameOf(DeclarationModel model, string collection)
    {
        var ns = ArchetypeNames.NamespaceOf(model.Symbol);
        var name = NameOf(model, collection);

        return ns.Length == 0 ? "global::" + name : $"global::{ns}.{name}";
    }

    /// <summary>The collections a shape holds, declared or named on a component it borrows.</summary>
    public static IEnumerable<(string Name, string? Held, IReadOnlyList<ForwardModel> Members)> Of(DeclarationModel model)
    {
        if (!model.IsReplicated)
            yield break;

        if (model.EmitsComponent)
        {
            foreach (var accessor in model.Accessors.Where(accessor => accessor.IsNativeContainer))
                yield return (accessor.Name, accessor.Type, NativeCollectionForwards.For(accessor, model.Name));

            yield break;
        }

        foreach (var collection in model.Collections)
        {
            var members = model.Forwards.Where(forward => forward.Name.Contains(collection)).ToList();

            yield return (collection, HeldAs(members, collection), members);
        }
    }

    /// <summary>What the component holds a borrowed collection in, read off its whole-collection setter.</summary>
    private static string? HeldAs(IReadOnlyList<ForwardModel> members, string collection)
    {
        foreach (var member in members)
            if (member.Name == "Set" + collection && member.Parameters.Count == 1)
                return member.Parameters[0].Type;

        return null;
    }

    public static void Emit(SourceWriter writer, DeclarationModel model)
    {
        foreach (var (collection, _, members) in Of(model))
            One(writer, model, collection, members);
    }

    private static void One(
        SourceWriter writer,
        DeclarationModel model,
        string collection,
        IReadOnlyList<ForwardModel> members)
    {
        var name = NameOf(model, collection);
        var handle = ArchetypeNames.EntityHandle;

        writer.Line();
        writer.Line($"/// <summary>The {collection} collection of a {model.Name}.</summary>");

        if (model.IsHidden)
            writer.Line(ArchetypeNames.Hidden);

        writer.Line("#if NET");
        writer.Line($"{model.Access} readonly ref struct {name}");
        writer.Line("#else");
        writer.Line($"{model.Access} readonly struct {name}");
        writer.Line("#endif");

        using (writer.Braces(string.Empty))
        {
            writer.Line($"private readonly {handle} _handle;");
            writer.Line();
            writer.Line("/// <exclude />");
            writer.Line($"public {name}(in {handle} handle) => _handle = handle;");

            foreach (var forward in members)
                Member(writer, forward, collection, model.QualifiedAccessors);
        }
    }

    /// <summary>The member as a collection reads it, with the value name taken back out.</summary>
    private static void Member(SourceWriter writer, ForwardModel forward, string value, string accessors)
    {
        var name = forward.Name.Replace(value, string.Empty);
        var call = $"{accessors}.{forward.Name}";

        writer.Line();

        if (forward.IsProperty)
        {
            writer.Line($"public {forward.ReturnType} {name} => {call}(_handle);");
            return;
        }

        writer.Line($"public {forward.Declaration(name)}");
        writer.Line($"    => {call}(_handle{forward.Separator}{forward.Arguments});");
    }
}
