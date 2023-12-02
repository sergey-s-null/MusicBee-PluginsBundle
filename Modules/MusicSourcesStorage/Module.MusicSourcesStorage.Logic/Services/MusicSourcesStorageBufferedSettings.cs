using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageBufferedSettings
    : IMusicSourcesStorageBufferedSettings
{
    public string VkDocumentsDownloadingDirectory { get; set; } = string.Empty;
    public string SourceFilesDownloadingDirectory { get; set; } = string.Empty;
    public int CoverSize { get; set; }
    public MetaDataType FileIdField { get; set; }

    private readonly IMusicSourcesStorageSettings _settings;

    public MusicSourcesStorageBufferedSettings(IMusicSourcesStorageSettings settings)
    {
        _settings = settings;

        Restore();
    }

    public void Restore()
    {
        VkDocumentsDownloadingDirectory = _settings.VkDocumentsDownloadingDirectory;
        SourceFilesDownloadingDirectory = _settings.SourceFilesDownloadingDirectory;
        CoverSize = _settings.CoverSize;
        FileIdField = _settings.FileIdField;
    }

    public void Save()
    {
        _settings.VkDocumentsDownloadingDirectory = VkDocumentsDownloadingDirectory;
        _settings.SourceFilesDownloadingDirectory = SourceFilesDownloadingDirectory;
        _settings.CoverSize = CoverSize;
        _settings.FileIdField = FileIdField;
    }
}