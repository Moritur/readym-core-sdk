using System.ComponentModel;

namespace ReadyM.SDK.Mods;

/// <summary>Implemented by the generated half of a [ModEntry] class.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IModEntry
{
    void SetModDirectory(string directory);

    void Init();
}
