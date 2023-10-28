using AutoMapper;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using VkNet.Abstractions;
using VkNet.Model.Attachments;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkService : IVkService
{
    private readonly IVkApi _vkApi;
    private readonly IMapper _mapper;

    public VkService(IVkApi vkApi, IMapper mapper)
    {
        _vkApi = vkApi;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<VkDocumentModel>> GetAttachedDocumentsFromPostAsync(
        long postOwnerId,
        long postId,
        CancellationToken token)
    {
        var post = await GetPostByIdAsync(postOwnerId, postId, token);

        return post.Attachments
            .Select(x => x.Instance)
            .OfType<Document>()
            .Select(x => _mapper.Map<VkDocumentModel>(x))
            .ToList();
    }

    public string GetPostGlobalIdString(long ownerId, long localId)
    {
        return $"-{ownerId}_{localId}";
    }

    public string GetPostGlobalIdString(VkPostGlobalId id)
    {
        return GetPostGlobalIdString(id.OwnerId, id.LocalId);
    }

    private async Task<Post> GetPostByIdAsync(long postOwnerId, long postId, CancellationToken token)
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