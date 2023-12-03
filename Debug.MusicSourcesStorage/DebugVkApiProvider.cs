using System;
using Module.Vk.Gui.Services.Abstract;
using VkNet.Abstractions;

namespace Debug.MusicSourcesStorage;

public sealed class DebugVkApiProvider : IVkApiProvider
{
    private readonly IVkApi _vkApi;

    public DebugVkApiProvider(IVkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public bool TryProvideAuthorizedVkApi(out IVkApi vkApi)
    {
        if (!_vkApi.IsAuthorized)
        {
            throw new InvalidOperationException("Vk api is not authorized.");
        }

        vkApi = _vkApi;
        return true;
    }
}