using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ReadyM.Api.Generators;

/// <summary>Reads the doc comment a symbol was declared with, from its syntax.</summary>
internal static class DocComments
{
    /// <summary>
    /// The `///` lines above the symbol's declaration, trimmed, or none. Read from the syntax rather than the symbol,
    /// which only has its comment when the project writes a documentation file.
    /// </summary>
    public static IReadOnlyList<string> Of(ISymbol symbol)
    {
        foreach (var reference in symbol.DeclaringSyntaxReferences)
        {
            var node = reference.GetSyntax();

            // A field's symbol points at its declarator; the comment sits on the whole declaration.
            if (node is VariableDeclaratorSyntax { Parent.Parent: FieldDeclarationSyntax field })
                node = field;

            var lines = node.GetLeadingTrivia().ToFullString()
                .Split('\n')
                .Select(line => line.Trim())
                .Where(line => line.StartsWith("///") && !line.StartsWith("////"))
                .ToList();

            if (lines.Count > 0)
                return lines;
        }

        return [];
    }
}
