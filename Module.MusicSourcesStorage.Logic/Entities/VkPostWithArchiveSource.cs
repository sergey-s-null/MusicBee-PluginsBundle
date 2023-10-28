namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class VkPostWithArchiveSource : MusicSource
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static VkPostWithArchiveSource New(
        string name,
        IReadOnlyList<SourceFile> files,
        VkPost post,
        VkDocument document)
    {
        return new VkPostWithArchiveSource(
            0,
            name,
            files,
            post,
            document
        );
    }

    public VkPost Post { get; }

    public VkDocument Document { get; }

    public VkPostWithArchiveSource(
        int id,
        string name,
        IReadOnlyList<SourceFile> files,
        VkPost post,
        VkDocument document)
        : base(id, name, files)
    {
        Post = post;
        Document = document;
    }
}