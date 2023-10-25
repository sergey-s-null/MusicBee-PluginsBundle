using Module.MusicSourcesStorage.Database.Models;

namespace Module.MusicSourcesStorage.Database.Services.Abstract;

public interface IMusicSourcesRepository
{
    Task AddAsync(MusicSource musicSource, CancellationToken token = default);
}