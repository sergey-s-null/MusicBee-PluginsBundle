using AutoMapper;
using Module.MusicSourcesStorage.Database;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using File = Module.MusicSourcesStorage.Database.Models.File;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageService : IMusicSourcesStorageService
{
    private readonly IMapper _mapper;
    private readonly Func<MusicSourcesStorageContext> _contextFactory;

    public MusicSourcesStorageService(
        IMapper mapper,
        Func<MusicSourcesStorageContext> contextFactory)
    {
        _mapper = mapper;
        _contextFactory = contextFactory;
    }

    public async Task AddMusicSourceAsync(
        VkPostGlobalId postId,
        VkDocument selectedDocument,
        IReadOnlyList<MusicSourceFile> files,
        CancellationToken token = default)
    {
        var musicSource = CreateMusicSourceModel(postId, selectedDocument, files);

        using var context = _contextFactory();
        context.Sources.Add(musicSource);
        await context.SaveChangesAsync(token);
    }

    private VkPostWithArchiveSource CreateMusicSourceModel(
        VkPostGlobalId postId,
        VkDocument selectedDocument,
        IReadOnlyList<MusicSourceFile> files)
    {
        return new VkPostWithArchiveSource
        {
            // todo use another name
            Name = $"{postId.OwnerId}_{postId.LocalId}",
            PostInfo = new VkPostInfo
            {
                Id = postId.LocalId,
                OwnerId = postId.OwnerId
            },
            SelectedDocumentInfo = _mapper.Map<VkDocumentInfo>(selectedDocument),
            Files = CreateFileModels(files)
        };
    }

    private IList<File> CreateFileModels(IReadOnlyList<MusicSourceFile> files)
    {
        return files
            .Select(CreateFileModel)
            .ToList();
    }

    private File CreateFileModel(MusicSourceFile file)
    {
        return file.Type switch
        {
            FileType.MusicFile => _mapper.Map<MusicFile>(file),
            FileType.Image => _mapper.Map<ImageFile>(file),
            FileType.Unknown => _mapper.Map<UnknownFile>(file),
            _ => throw new ArgumentOutOfRangeException(
                nameof(file.Type),
                file.Type,
                "Got unknown file type."
            )
        };
    }
}