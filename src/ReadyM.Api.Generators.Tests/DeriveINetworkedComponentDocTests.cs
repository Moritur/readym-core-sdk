using Xunit;

namespace ReadyM.Api.Generators.Tests;

/// <summary>The docs a component's generated properties carry, which shapes over the component inherit.</summary>
public sealed class DeriveINetworkedComponentDocTests(ITestOutputHelper output)
{
    private string Generated(string fields)
    {
        var source = $$"""
            using ReadyM.Api.Multiplayer.Generators;

            namespace ReadyM.Api.Generators.Tests.TestTypes;

            [DeriveINetworkedComponent]
            public partial struct StockComponent
            {
                {{fields}}
            }
            """;

        var result = SourceGeneratorTestHelper.RunGenerator<DeriveINetworkedComponentGenerator>(source, output);

        return string.Concat(result.GeneratedSyntaxTrees.Select(tree => tree.ToString()));
    }

    /// <summary>Copied rather than inherited: the field is private, so the API reference cannot follow a cref to it.</summary>
    [Fact]
    public void A_property_carries_its_field_docs()
    {
        var generated = Generated("""
            /// <summary>How much is left.</summary>
            private int _amount;
            """);

        Assert.Contains("/// <summary>How much is left.</summary>", generated);
        Assert.DoesNotContain("<inheritdoc", generated);
    }

    [Fact]
    public void A_property_over_an_undocumented_field_has_no_docs()
    {
        var generated = Generated("private int _amount;");

        Assert.DoesNotContain("<inheritdoc", generated);
        Assert.DoesNotContain("<summary>", generated);
    }
}
