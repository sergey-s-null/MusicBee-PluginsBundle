using Module.MusicSourcesStorage.Logic.Entities;
using VkNet.Model.Attachments;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class SpecificMappingExtensions
{
    public static VkDocument ToLogicModel(this Document document)
    {
        return new VkDocument(
            new VkOwnedEntityId(document.OwnerId!.Value, document.Id!.Value),
            document.Title,
            document.Uri,
            document.Size
        );
    }
}