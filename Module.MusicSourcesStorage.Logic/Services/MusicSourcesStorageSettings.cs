using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageSettings : IMusicSourcesStorageSettings
{
    public string VkDocumentsDownloadingDirectory
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public string SourceFilesDownloadingDirectory
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public int CoverSize
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public MetaDataType FileIdField
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
}