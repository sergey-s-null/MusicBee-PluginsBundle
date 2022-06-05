using System;
using System.Collections.Generic;
using System.Linq;
using Module.AudioSourcesComparer.DataClasses;
using Module.AudioSourcesComparer.Services.Abstract;
using Root.Helpers;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;
using Root.Services.Abstract;
using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Module.AudioSourcesComparer.Services
{
    public class VkToLocalComparerService : IVkToLocalComparerService
    {
        private const int AudiosPerRequest = 5000;

        private readonly IMusicBeeApi _musicBeeApi;
        private readonly IVkApi _vkApi;
        private readonly IVkApiAuthorizationsService _vkApiAuthorizationsService;

        public VkToLocalComparerService(
            IMusicBeeApi musicBeeApi,
            IVkApi vkApi,
            IVkApiAuthorizationsService vkApiAuthorizationsService)
        {
            _musicBeeApi = musicBeeApi;
            _vkApi = vkApi;
            _vkApiAuthorizationsService = vkApiAuthorizationsService;
        }

        public bool TryFindDifferences(out AudiosDifference? difference)
        {
            try
            {
                difference = FindDifferences();
                return true;
            }
            catch (Exception) // todo make specific exc
            {
                difference = null;
                return false;
            }
        }

        public AudiosDifference FindDifferences()
        {
            if (!_vkApiAuthorizationsService.AuthorizeVkApiIfNeeded())
            {
                throw new Exception("Could not auth vk api");
            }

            var res = _musicBeeApi.Library_QueryFilesEx("", out var files);
            if (!res)
            {
                throw new Exception("Err on Library_QueryFilesEx"); // todo
            }

            var mbAudiosByVkIds = GetMBFilesByVkIds(files);

            var vkIdsInMBLibrary = mbAudiosByVkIds.Keys.ToHashSet();

            var vkIdsInVk = GetVkIdsInVk();

            var vkOnlyVkIds = vkIdsInVk.ExceptCopy(vkIdsInMBLibrary);
            var mbOnlyVkIds = vkIdsInMBLibrary.ExceptCopy(vkIdsInVk);

            var vkOnly = _vkApi.Audio
                .Get(new AudioGetParams
                {
                    AudioIds = vkOnlyVkIds
                })
                .Select(MapToVkAudio)
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
                .ToDictionary(x => (long) x.VkId!, x => x.FilePath);
        }

        private ISet<long> GetVkIdsInVk()
        {
            return _vkApi.Audio
                .Get(new AudioGetParams
                {
                    Count = AudiosPerRequest,
                })
                .Select(x => x.Id)
                .Where(x => x is not null)
                .Select(x => (long) x!)
                .ToHashSet();
        }

        private VkAudio MapToVkAudio(Audio audio)
        {
            return new VkAudio(
                (long) audio.Id!, // todo check for null
                audio.Artist,
                audio.Title
            );
        }

        private MBAudio MapToMBAudio(string file, long vkId)
        {
            // todo rename
            var r1 = _musicBeeApi.TryGetIndex(file, out var index);
            if (!r1)
            {
                throw new Exception("chtoto ne tak"); // todo
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
}