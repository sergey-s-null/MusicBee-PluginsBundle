using AutoMapper;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageService : IMusicSourcesStorageService
{
    private readonly IMapper _mapper;
    private readonly IMusicSourcesStorage _musicSourcesStorage;

    public MusicSourcesStorageService(
        IMapper mapper,
        IMusicSourcesStorage musicSourcesStorage)
    {
        _mapper = mapper;
        _musicSourcesStorage = musicSourcesStorage;
    }

    public Task AddMusicSourceAsync(
        VkPostGlobalId postId,
        VkDocument selectedDocument,
        IReadOnlyList<SourceFile> files,
        CancellationToken token = default)
    {
        var musicSource = CreateMusicSourceModel(postId, selectedDocument, files);

        return _musicSourcesStorage.AddAsync(musicSource, token);
    }

    private VkPostWithArchiveSourceModel CreateMusicSourceModel(
        VkPostGlobalId postId,
        VkDocument selectedDocument,
        IReadOnlyList<SourceFile> files)
    {
        return new VkPostWithArchiveSourceModel
        {
            // todo use another name
            Name = $"{postId.OwnerId}_{postId.LocalId}",
            Post = new VkPostModel
            {
                Id = postId.LocalId,
                OwnerId = postId.OwnerId
            },
            Document = _mapper.Map<VkDocumentModel>(selectedDocument),
            Files = CreateFileModels(files)
        };
    }

    private List<FileModel> CreateFileModels(IReadOnlyList<SourceFile> files)
    {
        return files
            .Select(x => _mapper.Map<FileModel>(x))
            .ToList();
    }
}