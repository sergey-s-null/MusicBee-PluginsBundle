using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VkMusicDownloader.Ex;

namespace VkMusicDownloader.GUI
{
    public class AddingIncomingVM : BaseViewModel
    {
        #region Bindings

        private RelayCommand _refreshCmd;
        public RelayCommand RefreshCmd
            => _refreshCmd ?? (_refreshCmd = new RelayCommand(_ => Refresh()));

        private ObservableCollection<IncomingAudioVM> _incomingAudios;
        public ObservableCollection<IncomingAudioVM> IncomingAudios
            => _incomingAudios ?? (_incomingAudios = new ObservableCollection<IncomingAudioVM>());

        private ObservableCollection<MBAudioVM> _lastMBAudios;
        public ObservableCollection<MBAudioVM> LastMBAudios
            => _lastMBAudios ?? (_lastMBAudios = new ObservableCollection<MBAudioVM>());

        private RelayCommand _addToMBLibraryCmd;
        public RelayCommand AddToMBLibraryCmd
            => _addToMBLibraryCmd ?? (_addToMBLibraryCmd = new RelayCommand(arg =>
            {
                if (arg is IncomingAudioVM incomingAudio)
                    AddToMBLibrary(incomingAudio);
            }));

        #endregion

        private int _prevIndex = -1;

        private void AddToMBLibrary(IncomingAudioVM incomingAudio)
        {
            _prevIndex += 1;
            int currentIndex = _prevIndex;

            Plugin.CalcIndices(currentIndex, out int i1, out int i2);
            Plugin.MBApiInterface.Library_AddFileToLibrary(incomingAudio.FilePath, Plugin.LibraryCategory.Music);
            Plugin.SetIndex(incomingAudio.FilePath, currentIndex, false);
            Plugin.SetIndex1(incomingAudio.FilePath, i1, false);
            Plugin.SetIndex2(incomingAudio.FilePath, i2, false);
            Plugin.MBApiInterface.Library_CommitTagsToFile(incomingAudio.FilePath);

            Refresh();
        }

        private void Refresh()
        {
            IncomingAudios.Clear();
            LastMBAudios.Clear();

            IncomingAudioVM[] incomingAudios = GetIncomingAudios();
            MBAudioVM[] lastMBAudios = GetLastMBAudios();

            if (lastMBAudios.Length > 0)
                _prevIndex = lastMBAudios[0].Index;
            else
                _prevIndex = -1;

            IncomingAudios.AddRange(incomingAudios);
            LastMBAudios.AddRange(lastMBAudios);
        }

        private IncomingAudioVM[] GetIncomingAudios()
        {
            string query = $"<Source Type=\"4\"></Source>";
            Plugin.MBApiInterface.Library_QueryFilesEx(query, out string[] paths);

            return paths.Select(path => new IncomingAudioVM()
            {
                FilePath = path,
                Artist = Plugin.MBApiInterface.Library_GetFileTag(path, Plugin.MetaDataType.Artist),
                Title = Plugin.MBApiInterface.Library_GetFileTag(path, Plugin.MetaDataType.TrackTitle)
            }).ToArray();
        }

        private MBAudioVM[] GetLastMBAudios()
        {
            if (!Plugin.MBApiInterface.Library_QueryFilesEx("", out string[] paths))
                return Array.Empty<MBAudioVM>();

            var list = paths.Select(path =>
            {
                if (!Plugin.TryGetIndex(path, out int index))
                    return null;
                if (!Plugin.TryGetVkId(path, out long vkId))
                    vkId = -1;

                return new
                {
                    Index = index,
                    VkId = vkId,
                    Path = path
                };
            })
            .Where(item => item is object)
            .ToList();

            list.Sort((a, b) => b.Index.CompareTo(a.Index));

            int countOfLast = 3;
            if (list.Count > countOfLast)
                list.RemoveRange(countOfLast, list.Count - countOfLast);

            return list.Select(item => new MBAudioVM()
            {
                Artist = Plugin.MBApiInterface.Library_GetFileTag(item.Path, Plugin.MetaDataType.Artist),
                Title = Plugin.MBApiInterface.Library_GetFileTag(item.Path, Plugin.MetaDataType.TrackTitle),
                Index = item.Index,
                VkId = item.VkId
            }).ToArray();
        }

        
    }
}
