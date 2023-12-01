using Module.Core.Services.Abstract;
using Module.Vk.Services.Abstract;
using VkNet.Abstractions;

namespace Plugin.Main.Services;

// todo move to vk module after move VkApiAuthorizationsService
public sealed class VkApiProvider : IVkApiProvider
{
    private readonly IVkApi _vkApi;
    private readonly IVkApiAuthorizationsService _vkApiAuthorizationsService;

    public VkApiProvider(
        IVkApi vkApi,
        IVkApiAuthorizationsService vkApiAuthorizationsService)
    {
        _vkApi = vkApi;
        _vkApiAuthorizationsService = vkApiAuthorizationsService;
    }

    public bool TryProvideAuthorizedVkApi(out IVkApi vkApi)
    {
        // todo rework signature to pass vk api via arg?
        if (!_vkApiAuthorizationsService.AuthorizeVkApiIfNeeded())
        {
            vkApi = null!;
            return false;
        }

        vkApi = _vkApi;
        return true;
    }
}