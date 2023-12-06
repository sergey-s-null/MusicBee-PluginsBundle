using System.Data.Entity;
using Module.MusicSourcesStorage.Database.Factories.Abstract;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;

namespace Module.MusicSourcesStorage.Database.Services;

public sealed class MusicSourcesRepository : IMusicSourcesRepository
{
    private readonly IMusicSourcesStorageContextFactory _contextFactory;

    public MusicSourcesRepository(IMusicSourcesStorageContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<MusicSourceModel> AddAsync(MusicSourceModel musicSource, CancellationToken token)
    {
        using var context = _contextFactory.Create();

        var model = context.Sources.Add(musicSource);

        await context.SaveChangesAsync(token);

        return model;
    }

    public async Task DeleteAsync(int id, CancellationToken token)
    {
        using var context = _contextFactory.Create();

        var model = await context.Sources.FindAsync(token, id);
        context.Sources.Remove(model);

        await context.SaveChangesAsync(token);
    }

    public async Task<MusicSourceModel> GetSourceByFileIdAsync(int fileId, bool includeFiles, CancellationToken token)
    {
        using var context = _contextFactory.Create();

        var sources = includeFiles
            ? context.Sources.Include(x => x.Files)
            : context.Sources;

        var source = await sources.FirstOrDefaultAsync(
            source => source.Files.Any(file => file.Id == fileId),
            token
        );

        if (source is null)
        {
            throw new DatabaseException($"Could not find source for file with id {fileId}");
        }

        return source;
    }

    public async Task<IReadOnlyList<MusicSourceModel>> GetAllAsync(CancellationToken token)
    {
        using var context = _contextFactory.Create();

        return await context.Sources
            .Include(x => x.Files)
            .ToListAsync(token);
    }

    public async Task<MusicSourceAdditionalInfoModel?> FindAdditionalInfoAsync(int id, CancellationToken token)
    {
        using var context = _contextFactory.Create();

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
        using var context = _contextFactory.Create();

        var model = await context.Sources.FindAsync(token, id);
        if (model is null)
        {
            throw new DatabaseException($"Could not find music source with id {id}.");
        }

        model.AdditionalInfo = additionalInfo;
        await context.SaveChangesAsync(token);

        return model.AdditionalInfo;
    }

    public async Task<FileModel> GetSourceFileAsync(int fileId, bool includeSource, CancellationToken token)
    {
        using var context = _contextFactory.Create();

        var files = includeSource
            ? context.Files.Include(x => x.Source)
            : context.Files;

        var file = await files
            .Where(x => x.Id == fileId)
            .FirstOrDefaultAsync(token);

        if (file is null)
        {
            throw new DatabaseException($"Could not find source file with id {fileId}.");
        }

        return file;
    }

    public async Task<IReadOnlyList<FileModel>> ListSourceFilesBySourceIdAsync(
        int sourceId,
        bool includeSource,
        CancellationToken token)
    {
        using var context = _contextFactory.Create();

        var files = includeSource
            ? context.Files.Include(x => x.Source)
            : context.Files;

        return await files
            .Where(x => x.SourceId == sourceId)
            .ToListAsync(token);
    }

    public async Task SetMusicFileIsListenedAsync(int musicFileId, bool isListened, CancellationToken token)
    {
        using var context = _contextFactory.Create();

        var file = await context.Files.FindAsync(token, musicFileId);
        if (file is null)
        {
            throw new DatabaseException($"Could not find file with id {musicFileId}.");
        }

        if (file is not MusicFileModel musicFile)
        {
            throw new DatabaseException(
                $"File with id {musicFileId} is not a music file. " +
                $"Type of found model: {file.GetType()}."
            );
        }

        musicFile.IsListened = isListened;

        await context.SaveChangesAsync(token);
    }

    public async Task SetCoverAsync(int imageFileId, byte[] imageData, CancellationToken token)
    {
        using var context = _contextFactory.Create();

        var fileModel = await context.Files.FindAsync(token, imageFileId);
        if (fileModel is null)
        {
            throw new DatabaseException($"Could not find source file with id {imageFileId}.");
        }

        if (fileModel is not ImageFileModel imageFileModel)
        {
            throw new DatabaseException(
                $"Source file with id {imageFileId} is not image. " +
                $"Actual type: {fileModel.GetType()}."
            );
        }

        imageFileModel.IsCover = true;
        imageFileModel.Data = imageData;

        await context.SaveChangesAsync(token);
    }

    public async Task RemoveCoverAsync(int imageFileId, CancellationToken token = default)
    {
        using var context = _contextFactory.Create();

        var fileModel = await context.Files.FindAsync(token, imageFileId);
        if (fileModel is null)
        {
            throw new DatabaseException($"Could not find source file with id {imageFileId}.");
        }

        if (fileModel is not ImageFileModel imageFileModel)
        {
            throw new DatabaseException(
                $"Source file with id {imageFileId} is not image. " +
                $"Actual type: {fileModel.GetType()}."
            );
        }

        imageFileModel.IsCover = false;
        imageFileModel.Data = null;

        await context.SaveChangesAsync(token);
    }
}