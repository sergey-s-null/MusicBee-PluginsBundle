namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageBufferedSettings
    : IMusicSourcesStorageSettings
{
    void Restore();

    void Save();
}