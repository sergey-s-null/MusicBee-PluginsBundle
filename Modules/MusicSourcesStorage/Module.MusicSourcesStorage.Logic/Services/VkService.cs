using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Vk.Gui.Services.Abstract;
using VkNet.Model.Attachments;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkService : IVkService
{
    private readonly IVkApiProvider _vkApiProvider;

    public VkService(IVkApiProvider vkApiProvider)
    {
        _vkApiProvider = vkApiProvider;
    }

    public async Task<IReadOnlyList<VkDocument>> GetAttachedDocumentsFromPostAsync(
        long postOwnerId,
        long postId,
        CancellationToken token)
    {
        var post = await GetPostByIdAsync(postOwnerId, postId, token);

        return post.Attachments
            .Select(x => x.Instance)
            .OfType<Document>()
            .Select(x => x.ToLogicModel())
            .ToList();
    }

    public string GetPostGlobalIdString(long ownerId, long localId)
    {
        return $"-{ownerId}_{localId}";
    }

    public string GetPostGlobalIdString(VkOwnedEntityId id)
    {
        return GetPostGlobalIdString(id.OwnerId, id.Id);
    }

    private async Task<Post> GetPostByIdAsync(long postOwnerId, long postId, CancellationToken token)
    {
        if (!_vkApiProvider.TryProvideAuthorizedVkApi(out var vkApi))
        {
            throw new NotAuthorizedException("Could not get authorized Vk Api.");
        }

        var result = await Task.Run(
            () => vkApi.Wall.GetById(new[] { GetPostGlobalIdString(postOwnerId, postId) }),
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