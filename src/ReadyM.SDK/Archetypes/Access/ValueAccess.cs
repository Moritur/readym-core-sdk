using Friflo.Engine.ECS;
using ReadyM.Api.Mapping.Data;
using ReadyM.Api.Mapping.Tags;
using ReadyM.SDK.Entities;

namespace ReadyM.SDK.Archetypes.Access;

internal sealed class ValueAccess<TComponent, TValue>(Field<TComponent, TValue> field) : ValueAccess<TValue>
    where TComponent : struct, IReadyComponent
{
    internal override Type Component { get; } = typeof(TComponent);

    internal override int Field { get; } = field;

    internal override bool Write(in EntityHandle handle, TValue value, WriteKind kind)
        => handle.Write(field, value, kind);

    internal override TValue Read(in EntityHandle handle) => field.Get(handle.GetComponent<TComponent>());

    internal override void Set(in EntityHandle handle, TValue value)
        => field.Set(ref handle.GetComponent<TComponent>(), value);

    internal override bool WasSetFromApi(in EntityHandle handle)
        => field.WasSetFromApi(handle.GetComponent<TComponent>());

    internal override void ClearApiFlag(in EntityHandle handle)
        => handle.GetComponent<TComponent>().ClearApiFlag(field);
}

/// <summary>
/// The value its component is indexed by. Friflo reads the key as the component is added, so a write
/// that changed it where it lies would leave the entity filed under the key it used to hold.
/// </summary>
internal sealed class IndexedValueAccess<TComponent, TKey>(Field<TComponent, TKey> field) : ValueAccess<TKey>
    where TComponent : struct, IReadyComponent, IIndexedComponent<TKey>
{
    internal override Type Component { get; } = typeof(TComponent);

    internal override int Field { get; } = field;

    internal override bool Write(in EntityHandle handle, TKey value, WriteKind kind)
    {
        var updated = handle.GetComponent<TComponent>();

        if (!handle.Write(ref updated, field, value, kind))
            return false;

        handle.ReplaceIndexed<TComponent, TKey>(updated);

        return true;
    }

    internal override TKey Read(in EntityHandle handle) => field.Get(handle.GetComponent<TComponent>());

    internal override void Set(in EntityHandle handle, TKey value)
    {
        var updated = handle.GetComponent<TComponent>();

        field.Set(ref updated, value);
        handle.ReplaceIndexed<TComponent, TKey>(updated);
    }

    internal override bool WasSetFromApi(in EntityHandle handle)
        => field.WasSetFromApi(handle.GetComponent<TComponent>());

    internal override void ClearApiFlag(in EntityHandle handle)
        => handle.GetComponent<TComponent>().ClearApiFlag(field);
}

/// <summary>
/// What a token can ask about a value without knowing its type, which is what a collection needs:
/// it is never handed over, only asked about and then changed where it lies.
/// </summary>
internal abstract class ValueAccess
{
    internal abstract Type Component { get; }

    internal abstract int Field { get; }

    /// <summary>Is the API mask non-zero?</summary>
    internal abstract bool WasSetFromApi(in EntityHandle handle);

    /// <summary>Called after applying the value to the game.</summary>
    internal abstract void ClearApiFlag(in EntityHandle handle);
}

/// <summary>Allows accessing the value without the caller knowing which component it sits in.</summary>
internal abstract class ValueAccess<TValue> : ValueAccess
{

    /// <summary>Writes, going through the policy and set-from-API checks.</summary>
    internal abstract bool Write(in EntityHandle handle, TValue value, WriteKind kind);

    internal abstract TValue Read(in EntityHandle handle);

    /// <summary>Unconditional write without touching the API mask.</summary>
    internal abstract void Set(in EntityHandle handle, TValue value);
}