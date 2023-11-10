using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class DbToLogicMappingExtensions
{
    public static MusicSource ToLogicModel(this MusicSourceModel musicSourceModel)
    {
        return musicSourceModel switch
        {
            VkPostWithArchiveSourceModel vkPostWithArchiveSourceModel => vkPostWithArchiveSourceModel.ToLogicModel(),
            TorrentSourceModel torrentSourceModel => torrentSourceModel.ToLogicModel(),
            _ => throw new ArgumentOutOfRangeException(nameof(musicSourceModel))
        };
    }

    public static VkPostWithArchiveSource ToLogicModel(this VkPostWithArchiveSourceModel model)
    {
        return new VkPostWithArchiveSource(
            model.Id,
            model.AdditionalInfo.ToLogicModel(),
            model.Files
                .Select(x => x.ToLogicModel())
                .ToList(),
            model.Post.ToLogicModel(),
            model.Document.ToLogicModel()
        );
    }

    public static TorrentSource ToLogicModel(this TorrentSourceModel model)
    {
        return new TorrentSource(
            model.Id,
            model.AdditionalInfo.ToLogicModel(),
            model.Files
                .Select(x => x.ToLogicModel())
                .ToList(),
            model.TorrentFile
        );
    }

    public static MusicSourceAdditionalInfo ToLogicModel(this MusicSourceAdditionalInfoModel model)
    {
        return new MusicSourceAdditionalInfo(model.Name);
    }

    public static SourceFile ToLogicModel(this FileModel model)
    {
        return model switch
        {
            MusicFileModel musicFileModel => musicFileModel.ToLogicModel(),
            ImageFileModel imageFileModel => imageFileModel.ToLogicModel(),
            UnknownFileModel unknownFileModel => unknownFileModel.ToLogicModel(),
            _ => throw new ArgumentOutOfRangeException(nameof(model))
        };
    }

    public static MusicFile ToLogicModel(this MusicFileModel model)
    {
        return new MusicFile(
            model.Id,
            model.SourceId,
            model.Path,
            model.Size,
            model.IsListened
        );
    }

    public static ImageFile ToLogicModel(this ImageFileModel model)
    {
        return new ImageFile(
            model.Id,
            model.SourceId,
            model.Path,
            model.Size,
            model.IsCover,
            model.Data
        );
    }

    public static UnknownFile ToLogicModel(this UnknownFileModel model)
    {
        return new UnknownFile(
            model.Id,
            model.SourceId,
            model.Path,
            model.Size
        );
    }

    public static VkPost ToLogicModel(this VkPostModel model)
    {
        return new VkPost(new VkOwnedEntityId(model.OwnerId, model.Id));
    }

    public static VkDocument ToLogicModel(this VkDocumentModel model)
    {
        return new VkDocument(
            new VkOwnedEntityId(model.OwnerId, model.Id),
            model.Name,
            model.Uri,
            model.Size
        );
    }
}