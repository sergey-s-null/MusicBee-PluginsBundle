using Mead.MusicBee.Api.Services.Abstract;
using Module.MusicBee.Extension.Helpers;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Services.Abstract;
using VkNet.Abstractions;
using VkNet.Model.Attachments;

namespace Module.VkAudioDownloader.Services;

public sealed class VkAudiosService : IVkAudiosService
{
    private readonly IMusicBeeApi _musicBeeApi;
    private readonly IVkApi _vkApi;

    public VkAudiosService(IMusicBeeApi musicBeeApi, IVkApi vkApi)
    {
        _musicBeeApi = musicBeeApi;
        _vkApi = vkApi;
    }

    public IAsyncEnumerable<Audio> GetVkAudiosNotContainingInLibraryAsync()
    {
        var vkIdsToIgnore = _musicBeeApi.EnumerateVkIdsInLibrary().ToHashSet();

        return _vkApi.Audio.AsAsyncEnumerable()
            .Where(x => x.Id is not null
                        && !vkIdsToIgnore.Contains(x.Id.Value));
    }

    public IAsyncEnumerable<Audio> GetVkAudiosContainingInIncomingAsync()
    {
        var vkIdsToAccept = _musicBeeApi.EnumerateVkIdsInIncoming().ToHashSet();

        return _vkApi.Audio.AsAsyncEnumerable()
            .Where(x => x.Id is not null
                        && vkIdsToAccept.Contains(x.Id.Value));
    }
}