using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class VkPostWithArchiveSource : MusicSource
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static VkPostWithArchiveSource New(
        MusicSourceAdditionalInfo additionalInfo,
        IReadOnlyList<SourceFile> files,
        VkPost post,
        VkDocument document)
    {
        return new VkPostWithArchiveSource(
            0,
            additionalInfo,
            files,
            post,
            document
        );
    }

    public VkPost Post { get; }

    public VkDocument Document { get; }

    public VkPostWithArchiveSource(
        int id,
        MusicSourceAdditionalInfo additionalInfo,
        IReadOnlyList<SourceFile> files,
        VkPost post,
        VkDocument document)
        : base(id, additionalInfo, MusicSourceType.VkPostWithArchive, files)
    {
        Post = post;
        Document = document;
    }
}