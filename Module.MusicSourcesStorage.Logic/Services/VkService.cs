using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using VkNet.Abstractions;
using VkNet.Model.Attachments;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkService : IVkService
{
    private readonly IVkApi _vkApi;

    public VkService(IVkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public async Task<IReadOnlyList<VkDocument>> GetAttachedDocumentsFromPostAsync(
        ulong postOwnerId,
        ulong postId,
        CancellationToken token)
    {
        var post = await GetPostByIdAsync(postOwnerId, postId, token);

        return post.Attachments
            .Select(x => x.Instance)
            .OfType<Document>()
            .Select(x => new VkDocument(x.Title, x.Uri, x.Size))
            .ToList();
    }

    public string GetPostGlobalIdString(ulong ownerId, ulong localId)
    {
        return $"-{ownerId}_{localId}";
    }

    public string GetPostGlobalIdString(VkPostGlobalId id)
    {
        return GetPostGlobalIdString(id.OwnerId, id.LocalId);
    }

    private async Task<Post> GetPostByIdAsync(ulong postOwnerId, ulong postId, CancellationToken token)
    {
        var result = await Task.Run(
            () => _vkApi.Wall.GetById(new[] { GetPostGlobalIdString(postOwnerId, postId) }),
            token
        );

        if (result is null)
        {
            throw new VkServiceException("Got empty result from vk api.");
        }

        var posts = result.WallPosts;
        if (posts.Count != 1)
        {
            throw new VkServiceException($"Expected single post. Got {posts.Count} posts.");
        }

        return posts.Single();
    }
}