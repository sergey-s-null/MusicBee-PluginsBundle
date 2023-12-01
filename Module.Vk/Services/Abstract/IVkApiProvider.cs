using VkNet.Abstractions;

namespace Module.Vk.Services.Abstract;

public interface IVkApiProvider
{
    bool TryProvideAuthorizedVkApi(out IVkApi vkApi);
}