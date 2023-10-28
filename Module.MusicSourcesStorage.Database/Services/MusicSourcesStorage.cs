using System.Data.Entity;
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

    public async Task AddAsync(MusicSourceModel musicSource, CancellationToken token)
    {
        using var context = _contextFactory();

        context.Sources.Add(musicSource);

        await context.SaveChangesAsync(token);
    }

    public async Task<IReadOnlyList<MusicSourceModel>> GetAllAsync(CancellationToken token = default)
    {
        using var context = _contextFactory();

        return await context.Sources
            .Include(x => x.Files)
            .ToListAsync(token);
    }
}