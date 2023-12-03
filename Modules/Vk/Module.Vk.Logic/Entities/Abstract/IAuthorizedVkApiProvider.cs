using VkNet.Abstractions;

namespace Module.Vk.Logic.Entities.Abstract;

public interface IAuthorizedVkApiProvider
{
    IVkApi AuthorizedVkApi { get; }
}