using VkNet.Abstractions;

namespace Module.Vk.Gui.Services.Abstract;

public interface IVkApiProvider
{
    bool TryProvideAuthorizedVkApi(out IVkApi vkApi);
}