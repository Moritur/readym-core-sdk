using System.Runtime.CompilerServices;

namespace ReadyM.SDK.Entities;

/// <summary>
/// Reference to a single component instance.
/// Either an array + index (client-side) or a raw pointer (server side).
/// </summary>
internal readonly struct ComponentRef
{
    private readonly Array? _heap;
    private readonly IntPtr _data;
    private readonly int _index;

    /// <summary>An array a managed runtime owns.</summary>
    internal ComponentRef(Array heap, int index)
    {
        _heap = heap;
        _data = IntPtr.Zero;
        _index = index;
    }

    /// <summary>Memory that does not move, like on the AOT-compiled server.</summary>
    internal ComponentRef(IntPtr data)
    {
        _heap = null;
        _data = data;
        _index = 0;
    }

    internal bool Found => _heap is not null || _data != IntPtr.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe ref T As<T>() where T : struct
        => ref _heap is null
            ? ref Unsafe.AsRef<T>((void*)_data)
            : ref Unsafe.As<T[]>(_heap)[_index];
}