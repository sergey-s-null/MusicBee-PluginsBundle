using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Module.VkMusicDownloader.Helpers;
using PropertyChanged;
using Root;
using Root.MVVM;

namespace Module.VkMusicDownloader.GUI.MusicDownloaderWindow.AddingIncoming
{
    [AddINotifyPropertyChangedInterface]
    public class AddingIncomingVM
    {
        #region Bindings

        // TODO используется ли вообще
        private RelayCommand _refreshCmd;
        public RelayCommand RefreshCmd
            => _refreshCmd ??= new RelayCommand(_ => Refresh());

        public ObservableCollection<IncomingAudioVM> IncomingAudios { get; } = new();

        public ObservableCollection<MBAudioVM> LastMBAudios { get; } = new();

        #endregion

        private readonly MusicBeeApiInterface _mbApi;
        
        private int _prevIndex = -1;

        public AddingIncomingVM(MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }
        
        private void Refresh()
        {
            IncomingAudios.Clear();
            LastMBAudios.Clear();

            var incomingAudios = GetIncomingAudios();
            var lastMBAudios = GetLastMBAudios();

            if (lastMBAudios.Length > 0)
                _prevIndex = lastMBAudios[0].Index;
            else
                _prevIndex = -1;

            
            IncomingAudios.AddRange(incomingAudios);
            LastMBAudios.AddRange(lastMBAudios);
        }

        private IReadOnlyCollection<IncomingAudioVM> GetIncomingAudios()
        {
            var query = $"<Source Type=\"4\"></Source>";
            _mbApi.Library_QueryFilesEx(query, out var paths);

            return paths
                .Select(path => new IncomingAudioVM()
                {
                    FilePath = path,
                    Artist = _mbApi.Library_GetFileTag(path, MetaDataType.Artist),
                    Title = _mbApi.Library_GetFileTag(path, MetaDataType.TrackTitle)
                })
                .Select(a =>
                {
                    a.OnAddToMBLibrary += (sender, _) => AddToMBLibrary((IncomingAudioVM) sender);
                    return a;
                })
                .ToList()
                .AsReadOnly();
        }

        private void AddToMBLibrary(IncomingAudioVM incomingAudio)
        {
            _prevIndex += 1;
            var currentIndex = _prevIndex;

            MBApiHelper.CalcIndices(currentIndex, out var i1, out var i2);
            _mbApi.Library_AddFileToLibrary(incomingAudio.FilePath, LibraryCategory.Music);
            _mbApi.SetIndex(incomingAudio.FilePath, currentIndex, false);
            _mbApi.SetIndex1(incomingAudio.FilePath, i1, false);
            _mbApi.SetIndex2(incomingAudio.FilePath, i2, false);
            _mbApi.Library_SetFileTag(incomingAudio.FilePath, MetaDataType.Artist, incomingAudio.Artist);
            _mbApi.Library_SetFileTag(incomingAudio.FilePath, MetaDataType.TrackTitle, incomingAudio.Title);
            _mbApi.Library_CommitTagsToFile(incomingAudio.FilePath);

            Refresh();
        }
        
        private MBAudioVM[] GetLastMBAudios()
        {
            if (!_mbApi.Library_QueryFilesEx("", out var paths))
                return Array.Empty<MBAudioVM>();

            var list = paths.Select(path =>
            {
                if (!_mbApi.TryGetIndex(path, out var index))
                    return null;
                if (!_mbApi.TryGetVkId(path, out var vkId))
                    vkId = -1;

                return new
                {
                    Index = index,
                    VkId = vkId,
                    Path = path
                };
            })
            .Where(item => item is not null)
            .ToList();

            list.Sort((a, b) => b.Index.CompareTo(a.Index));

            var countOfLast = 3;
            if (list.Count > countOfLast)
                list.RemoveRange(countOfLast, list.Count - countOfLast);

            return list.Select(item => new MBAudioVM()
            {
                Artist = _mbApi.Library_GetFileTag(item.Path, MetaDataType.Artist),
                Title = _mbApi.Library_GetFileTag(item.Path, MetaDataType.TrackTitle),
                Index = item.Index,
                VkId = item.VkId
            }).ToArray();
        }
    }
}
