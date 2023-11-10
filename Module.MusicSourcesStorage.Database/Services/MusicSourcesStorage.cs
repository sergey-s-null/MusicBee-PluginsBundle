﻿using System.Data.Entity;
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

    public async Task<MusicSourceModel> AddAsync(MusicSourceModel musicSource, CancellationToken token)
    {
        using var context = _contextFactory();

        var model = context.Sources.Add(musicSource);

        await context.SaveChangesAsync(token);

        return model;
    }

    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        using var context = _contextFactory();

        var model = await context.Sources.FindAsync(token, id);
        context.Sources.Remove(model);

        await context.SaveChangesAsync(token);
    }

    public async Task<IReadOnlyList<MusicSourceModel>> GetAllAsync(CancellationToken token = default)
    {
        using var context = _contextFactory();

        return await context.Sources
            .Include(x => x.Files)
            .ToListAsync(token);
    }

    public async Task<MusicSourceAdditionalInfoModel?> FindAdditionalInfoAsync(int id, CancellationToken token)
    {
        using var context = _contextFactory();

        var model = await context.Sources.FindAsync(token, id);

        return model?.AdditionalInfo;
    }

    public async Task<MusicSourceAdditionalInfoModel> GetAdditionalInfoAsync(int id, CancellationToken token)
    {
        var additionalInfo = await FindAdditionalInfoAsync(id, token);

        if (additionalInfo is null)
        {
            throw new DatabaseException($"Could not find source additional info for source with id {id}.");
        }

        return additionalInfo;
    }

    public async Task<MusicSourceAdditionalInfoModel> UpdateAdditionalInfo(
        int id,
        MusicSourceAdditionalInfoModel additionalInfo,
        CancellationToken token)
    {
        using var context = _contextFactory();

        var model = await context.Sources.FindAsync(token, id);
        if (model is null)
        {
            throw new DatabaseException($"Could not find music source with id {id}.");
        }

        model.AdditionalInfo = additionalInfo;
        await context.SaveChangesAsync(token);

        return model.AdditionalInfo;
    }
}