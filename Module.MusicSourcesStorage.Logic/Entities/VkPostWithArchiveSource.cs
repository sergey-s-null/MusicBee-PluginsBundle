namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class VkPostWithArchiveSource : MusicSource
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static VkPostWithArchiveSource New(
        string name,
        IReadOnlyList<SourceFile> files,
        VkPostInfo vkPostInfo,
        VkDocument document)
    {
        return new VkPostWithArchiveSource(
            0,
            name,
            files,
            vkPostInfo,
            document
        );
    }

    public VkPostInfo PostInfo { get; }

    public VkDocument Document { get; }

    public VkPostWithArchiveSource(
        int id,
        string name,
        IReadOnlyList<SourceFile> files,
        VkPostInfo postInfo,
        VkDocument document)
        : base(id, name, files)
    {
        PostInfo = postInfo;
        Document = document;
    }
}