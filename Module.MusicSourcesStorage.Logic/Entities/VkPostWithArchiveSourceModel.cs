namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class VkPostWithArchiveSourceModel : MusicSourceModel
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static VkPostWithArchiveSourceModel New(
        string name,
        IReadOnlyList<FileModel> files,
        VkPostInfoModel vkPostInfo,
        VkDocumentModel document)
    {
        return new VkPostWithArchiveSourceModel(
            0,
            name,
            files,
            vkPostInfo,
            document
        );
    }

    public VkPostInfoModel PostInfo { get; }

    public VkDocumentModel Document { get; }

    public VkPostWithArchiveSourceModel(
        int id,
        string name,
        IReadOnlyList<FileModel> files,
        VkPostInfoModel postInfo,
        VkDocumentModel document)
        : base(id, name, files)
    {
        PostInfo = postInfo;
        Document = document;
    }
}