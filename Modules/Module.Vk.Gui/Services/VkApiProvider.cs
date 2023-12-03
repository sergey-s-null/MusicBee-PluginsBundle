using Module.Vk.Gui.Services.Abstract;
using VkNet.Abstractions;

namespace Module.Vk.Gui.Services;

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