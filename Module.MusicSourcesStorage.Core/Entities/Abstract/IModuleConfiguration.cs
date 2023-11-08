namespace Module.MusicSourcesStorage.Core.Entities.Abstract;

public interface IModuleConfiguration
{
    string VkDocumentsDownloadingDirectory { get; }
    string SourceFilesDownloadingDirectory { get; }
    string DatabaseConnectionString { get; }
}