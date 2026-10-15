using System.ComponentModel;

namespace ReadyM.Api.Saves;

/// <exclude />
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISaveSerializable
{
    void WriteSave(ISaveWriter writer, ISaveWriteContext context);
    void ReadSave(ISaveReader reader, ISaveLoadContext context);
}
