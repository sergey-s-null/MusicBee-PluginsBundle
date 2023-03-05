using Mead.MusicBee.Api.Services.Abstract;
using Module.MusicBee.Extension.Helpers;
using Module.VkAudioDownloader.Entities;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Services.Abstract;
using VkNet.Abstractions;
using VkNet.Model.Attachments;

namespace Module.VkAudioDownloader.Services;

public sealed class VkAudiosService : IVkAudiosService
{
    private const int AllAudiosPerRequest = 5000;
    
    private readonly IMusicBeeApi _musicBeeApi;
    private readonly IVkApi _vkApi;

    public VkAudiosService(IMusicBeeApi musicBeeApi, IVkApi vkApi)
    {
        _musicBeeApi = musicBeeApi;
        _vkApi = vkApi;
    }

    public async Task<IReadOnlyList<VkAudioModel>> GetVkAudiosToDisplayAsync()
    {
        var vkIdsInLibrary = _musicBeeApi.EnumerateVkIdsInLibrary().ToHashSet();
        var vkIdsInIncoming = _musicBeeApi.EnumerateVkIdsInIncoming().ToHashSet();

        return await _vkApi.Audio.AsAsyncEnumerable(AllAudiosPerRequest)
            .Where(x => x.Id is not null
                        && !vkIdsInLibrary.Contains(x.Id.Value))
            .Select(x => Map(x, vkIdsInIncoming.Contains(x.Id!.Value)))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<VkAudioModel>> GetFirstVkAudiosToDisplayAsync()
    {
        var vkIdsInLibrary = _musicBeeApi.EnumerateVkIdsInLibrary().ToHashSet();
        var vkIdsInIncoming = _musicBeeApi.EnumerateVkIdsInIncoming().ToHashSet();

        return await _vkApi.Audio.AsAsyncEnumerable()
            .Where(x => x.Id is not null)
            .TakeWhile(x => !vkIdsInLibrary.Contains(x.Id!.Value))
            .Select(x => Map(x, vkIdsInIncoming.Contains(x.Id!.Value)))
            .ToListAsync();
    }

    private static VkAudioModel Map(Audio audio, bool isInIncoming)
    {
        return new VkAudioModel(
            audio.Id!.Value,
            audio.Artist,
            audio.Title,
            audio.Url.AbsoluteUri,
            isInIncoming
        );
    }
}