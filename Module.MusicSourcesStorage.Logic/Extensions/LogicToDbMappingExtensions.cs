using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class LogicToDbMappingExtensions
{
    public static MusicSourceModel ToDbModel(this MusicSource musicSource)
    {
        return musicSource switch
        {
            VkPostWithArchiveSource vkPostWithArchiveSource => vkPostWithArchiveSource.ToDbModel(),
            TorrentSource torrentSource => torrentSource.ToDbModel(),
            _ => throw new ArgumentOutOfRangeException(nameof(musicSource))
        };
    }

    public static VkPostWithArchiveSourceModel ToDbModel(this VkPostWithArchiveSource source)
    {
        return new VkPostWithArchiveSourceModel
        {
            Id = source.Id,
            AdditionalInfo = source.AdditionalInfo.ToDbModel(),
            Files = source.Files
                .Select(x => x.ToDbModel())
                .ToList(),
            Post = source.Post.ToDbModel(),
            Document = source.Document.ToDbModel()
        };
    }

    public static TorrentSourceModel ToDbModel(this TorrentSource source)
    {
        return new TorrentSourceModel
        {
            Id = source.Id,
            AdditionalInfo = source.AdditionalInfo.ToDbModel(),
            Files = source.Files
                .Select(f => f.ToDbModel())
                .ToList(),
            TorrentFile = source.TorrentFile
        };
    }

    public static MusicSourceAdditionalInfoModel ToDbModel(this MusicSourceAdditionalInfo additionalInfo)
    {
        return new MusicSourceAdditionalInfoModel
        {
            Name = additionalInfo.Name
        };
    }

    public static FileModel ToDbModel(this SourceFile sourceFile)
    {
        return sourceFile switch
        {
            MusicFile musicFile => musicFile.ToDbModel(),
            ImageFile imageFile => imageFile.ToDbModel(),
            UnknownFile unknownFile => unknownFile.ToDbModel(),
            _ => throw new ArgumentOutOfRangeException(nameof(sourceFile))
        };
    }

    public static MusicFileModel ToDbModel(this MusicFile musicFile)
    {
        return new MusicFileModel
        {
            Id = musicFile.Id,
            Path = musicFile.Path,
            Size = musicFile.Size,
            IsListened = musicFile.IsListened
        };
    }

    public static ImageFileModel ToDbModel(this ImageFile imageFile)
    {
        return new ImageFileModel
        {
            Id = imageFile.Id,
            Path = imageFile.Path,
            Size = imageFile.Size,
            IsCover = imageFile.IsCover,
            Data = imageFile.Data
        };
    }

    public static UnknownFileModel ToDbModel(this UnknownFile unknownFile)
    {
        return new UnknownFileModel
        {
            Id = unknownFile.Id,
            Path = unknownFile.Path,
            Size = unknownFile.Size
        };
    }

    public static VkPostModel ToDbModel(this VkPost vkPost)
    {
        return new VkPostModel
        {
            Id = vkPost.Id.Id,
            OwnerId = vkPost.Id.OwnerId
        };
    }

    public static VkDocumentModel ToDbModel(this VkDocument vkPost)
    {
        return new VkDocumentModel
        {
            Id = vkPost.Id.Id,
            OwnerId = vkPost.Id.OwnerId,
            Name = vkPost.Name,
            Uri = vkPost.Uri,
            Size = vkPost.Size
        };
    }
}