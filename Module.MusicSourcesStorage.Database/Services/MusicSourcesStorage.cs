using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;

namespace Module.MusicSourcesStorage.Database.Services;

public sealed class MusicSourcesStorage : IMusicSourcesStorage
{
    private readonly Func<MusicSourcesStorageContext> _contextFactory;

    public MusicSourcesStorage(Func<MusicSourcesStorageContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task AddAsync(MusicSource musicSource, CancellationToken token)
    {
        using var context = _contextFactory();

        context.Sources.Add(musicSource);

        await context.SaveChangesAsync(token);
    }
}