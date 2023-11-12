namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IFilesDeletingService
{
    // todo different logic for music files?
    Task DeleteAsync(int fileId, CancellationToken token = default);
}