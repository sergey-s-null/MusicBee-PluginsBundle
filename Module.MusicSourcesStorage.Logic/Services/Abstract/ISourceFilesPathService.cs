using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ISourceFilesPathService
{
    /// <summary>
    /// Return path where file should be located if it was downloaded. 
    /// </summary>
    string GetSourceFileTargetPath(MusicSourceAdditionalInfo sourceAdditionalInfo, SourceFile file);
}