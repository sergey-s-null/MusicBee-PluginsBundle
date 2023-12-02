namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IFilesDeletingService
{
    Task DeleteAsync(int fileId, CancellationToken token = default);
}