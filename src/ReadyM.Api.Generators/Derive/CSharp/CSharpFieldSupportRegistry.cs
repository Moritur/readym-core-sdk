using System.Text;
using Microsoft.CodeAnalysis;
using ReadyM.Api.Generators.Derive.CSharp.FieldSupport;
using ReadyM.Api.Generators.Derive.CSharp.Serialization;

namespace ReadyM.Api.Generators.Derive.CSharp;

internal class CSharpFieldSupportRegistry
{
    private static readonly ICSharpFieldTypeSupportImpl[] CSharpFieldSupportImpls =
    [
        new PrimitiveFieldTypeSupportImpl(),
        new EnumFieldTypeSupportImpl(),
        new VectorLikeFieldTypeSupportImpl(),
        new NativeStringFieldTypeSupportImpl(),
        new NativeDictionaryFieldTypeSupportImpl(),
        new NativeListFieldTypeSupportImpl(),
        new NativeContainerFieldTypeSupportImpl(),
        new DeltaEquatableFieldTypeSupportImpl(),
        new EquatableFieldTypeSupportImpl(),
    ];

    private static readonly ICSharpTypeSerializationImpl[] CSharpSerializationImpls =
    [
        new EntitySerializationImpl(),
        new PrimitiveSerializationImpl(),
        new EnumSerializationImpl(),
        new VectorLikeSerializationImpl(),
        new NativeListSerializationImpl(),
        new NativeDictionarySerializationImpl(),
        new NativeStringSerializationImpl(),
        new CustomMethodSerializationImpl(),
    ];

    internal static readonly ICSharpSerializationCodec NetDataCodec = new NetDataSerializationCodec();
    internal static readonly ICSharpSerializationCodec InternedNetDataCodec = new InternedNetDataSerializationCodec(NetDataCodec);
    internal static readonly ICSharpSerializationCodec SaveCodec = new SaveSerializationCodec();

    internal static readonly DeriveTypeSupportVisitorBase<ICSharpFieldTypeSupportImpl> FieldTypeSupportVisitor = new(
        CSharpFieldSupportImpls,
        new FallbackFieldTypeSupportImpl()
    );

    internal static readonly DeriveTypeSupportVisitor<ICSharpTypeSerializationImpl, CSharpEmitSerializeContext> EmitSerializeVisitor = new(
        CSharpSerializationImpls,
        new FallbackTypeSerializationImpl()
    );
    
    internal static readonly DeriveTypeSupportVisitor<ICSharpTypeSerializationImpl, CSharpEmitDeserializeContext> EmitDeserializeVisitor = new(
        CSharpSerializationImpls,
        new FallbackTypeSerializationImpl()
    );

    /// <summary>Whether a value of this type can be sent, rather than throwing when it is.</summary>
    internal static bool CanSend(ITypeSymbol type)
    {
        // An entity reference is supported only to say, at run time, that it cannot be sent.
        if (!EmitSerializeVisitor.TryGetImpl(type, false, out var impl) || impl is EntitySerializationImpl)
            return false;

        if (SerializationHelper.IsNativeList(type, out var item))
            return CanSend(item);

        if (SerializationHelper.IsNativeDictionary(type, out var key, out var value, out _))
            return CanSend(key) && CanSend(value);

        return true;
    }
}
