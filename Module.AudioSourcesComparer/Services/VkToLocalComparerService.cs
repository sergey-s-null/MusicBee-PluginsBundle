using Mead.MusicBee.Api.Services.Abstract;
using Mead.MusicBee.Enums;
using Module.AudioSourcesComparer.DataClasses;
using Module.AudioSourcesComparer.Exceptions;
using Module.AudioSourcesComparer.Services.Abstract;
using Module.Core.Helpers;
using Module.MusicBee.Extension.Helpers;
using Module.Vk.Logic.Helpers;
using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Module.AudioSourcesComparer.Services;

public sealed class VkToLocalComparerService : IVkToLocalComparerService
{
    private const int AudiosPerRequest = 5000;

    private readonly IMusicBeeApi _musicBeeApi;
    private readonly IVkApi _vkApi;

    public VkToLocalComparerService(
        IMusicBeeApi musicBeeApi,
        IVkApi vkApi)
    {
        _musicBeeApi = musicBeeApi;
        _vkApi = vkApi;
    }

    public async Task<AudiosDifference> FindDifferencesAsync()
    {
        if (!_vkApi.IsAuthorizedWithCheck())
        {
            throw new VkApiUnauthorizedException("Vk api is not authorized.");
        }

        if (!_musicBeeApi.Library_QueryFilesEx("", out var files))
        {
            throw new MBApiException("Could not get audios list from music bee.");
        }

        var mbAudiosByVkIds = GetMBFilesByVkIds(files!);
        var vkIdsInMBLibrary = mbAudiosByVkIds.Keys.ToHashSet();

        var vkAudiosByVkIds = await GetVkAudiosByVkIdsAsync();
        var vkIdsInVk = vkAudiosByVkIds.Keys.ToHashSet();

        var vkOnlyVkIds = vkIdsInVk.ExceptCopy(vkIdsInMBLibrary);
        var mbOnlyVkIds = vkIdsInMBLibrary.ExceptCopy(vkIdsInVk);

        var vkOnly = vkOnlyVkIds
            .Select(x => MapToVkAudio(vkAudiosByVkIds[x]))
            .ToReadOnlyCollection();

        var mbOnly = mbOnlyVkIds
            .Select(x => MapToMBAudio(mbAudiosByVkIds[x], x))
            .ToReadOnlyCollection();

        return new AudiosDifference(vkOnly, mbOnly);
    }

    private IReadOnlyDictionary<long, string> GetMBFilesByVkIds(IReadOnlyCollection<string> files)
    {
        return files
            .Select(x => new
            {
                VkId = _musicBeeApi.GetVkIdOrNull(x),
                FilePath = x
            })
            .Where(x => x.VkId is not null)
            .ToDictionary(x => (long)x.VkId!, x => x.FilePath);
    }

    private async Task<IReadOnlyDictionary<long, Audio>> GetVkAudiosByVkIdsAsync()
    {
        var response = await _vkApi.Audio
            .GetAsync(new AudioGetParams
            {
                Count = AudiosPerRequest,
            });

        return response
            .Where(x => x.Id is not null)
            .ToDictionary(x => x.Id!.Value);
    }

    private VkAudio MapToVkAudio(Audio audio)
    {
        if (audio.Id is null)
        {
            throw new VkApiInvalidValueException(
                "Got vk audio with empty id.\n" +
                $"Artist: \"{audio.Artist}\". Title: \"{audio.Title}\".\n" +
                $"Audio: {audio}"
            );
        }

        return new VkAudio(
            audio.Id!.Value,
            audio.Artist,
            audio.Title
        );
    }

    private MBAudio MapToMBAudio(string file, long vkId)
    {
        if (!_musicBeeApi.TryGetIndex(file, out var index))
        {
            throw new MBLibraryInvalidStateException($"Could not get index of file with path \"{file}\".");
        }

        return new MBAudio(
            file,
            vkId,
            index,
            _musicBeeApi.Library_GetFileTag(file, MetaDataType.Artist),
            _musicBeeApi.Library_GetFileTag(file, MetaDataType.TrackTitle)
        );
    }
}