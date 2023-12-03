using Module.Vk.Logic.Entities.Abstract;
using VkNet.Abstractions;

namespace Module.Vk.Logic.Entities;

public sealed class AuthorizedVkApiProvider : IAuthorizedVkApiProvider
{
    public IVkApi AuthorizedVkApi { get; }

    public AuthorizedVkApiProvider(IVkApi authorizedVkApi)
    {
        AuthorizedVkApi = authorizedVkApi;
    }
}