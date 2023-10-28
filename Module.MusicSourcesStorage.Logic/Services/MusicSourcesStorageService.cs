using AutoMapper;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using FileModel = Module.MusicSourcesStorage.Database.Models.FileModel;
using ImageFileModel = Module.MusicSourcesStorage.Database.Models.ImageFileModel;
using MusicFileModel = Module.MusicSourcesStorage.Database.Models.MusicFileModel;
using UnknownFileModel = Module.MusicSourcesStorage.Database.Models.UnknownFileModel;
using VkDocumentModel = Module.MusicSourcesStorage.Logic.Entities.VkDocumentModel;
using VkPostWithArchiveSourceModel = Module.MusicSourcesStorage.Database.Models.VkPostWithArchiveSourceModel;

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
        VkDocumentModel selectedDocument,
        IReadOnlyList<IndexedFile> files,
        CancellationToken token = default)
    {
        var musicSource = CreateMusicSourceModel(postId, selectedDocument, files);

        return _musicSourcesStorage.AddAsync(musicSource, token);
    }

    private VkPostWithArchiveSourceModel CreateMusicSourceModel(
        VkPostGlobalId postId,
        VkDocumentModel selectedDocument,
        IReadOnlyList<IndexedFile> files)
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
            Document = _mapper.Map<Database.Models.VkDocumentModel>(selectedDocument),
            Files = CreateFileModels(files)
        };
    }

    private List<FileModel> CreateFileModels(IReadOnlyList<IndexedFile> files)
    {
        return files
            .Select(CreateFileModel)
            .ToList();
    }

    private FileModel CreateFileModel(IndexedFile file)
    {
        return file.Type switch
        {
            FileType.MusicFile => _mapper.Map<MusicFileModel>(file),
            FileType.Image => _mapper.Map<ImageFileModel>(file),
            FileType.Unknown => _mapper.Map<UnknownFileModel>(file),
            _ => throw new ArgumentOutOfRangeException(
                nameof(file.Type),
                file.Type,
                "Got unknown file type."
            )
        };
    }
}