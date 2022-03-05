using System;
using System.Collections.Generic;
using System.Windows.Input;
using Root.MVVM;

namespace Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow
{
    public class VkAudioDownloaderWindowDTVM : IVkAudioDownloaderWindowVM
    {
        public ICommand RefreshCmd { get; } =
            new RelayCommand(_ => throw new NotSupportedException());

        public ICommand ApplyCheckStateToSelectedCmd { get; } =
            new RelayCommand(_ => throw new NotSupportedException());

        public bool IsDownloading => false;

        public ICommand DownloadCmd { get; } =
            new RelayCommand(_ => throw new NotSupportedException());

        public IList<IAudioVM> Audios { get; }

        public VkAudioDownloaderWindowDTVM()
        {
            Audios = new IAudioVM[]
            {
                new VkAudioVM(9956700, "Last added Artist", "Last added Title", 0, "url", false),
                new VkAudioVM(8665701, "artist 1", "title 1", 1, "url", true),
                new VkAudioVM(6643702, "artist 2", "title 2", 2, "url", false),
                new VkAudioVM(0965703, "artist 3", "title 3", 3, "url", false),
                new VkAudioVM(3278704, "artist 4", "title 4", 4, "url", true),
                new VkAudioVM(3473705, "artist 5", "title 5", 5, "url", false),
                new VkAudioVM(3834706, "End of vk list", "title", 6, "url", false),
                new MBAudioVM(666, "Last Added MB Artist", "Title", 1024),
                new MBAudioVM(666, "602", "Title", 602),
                new MBAudioVM(666, "601", "Title", 601),
                new MBAudioVM(666, "600", "Title", 600),
                new MBAudioVM(666, "MB Middle artist", "Title", 512),
                new MBAudioVM(666, "2", "Title", 2),
                new MBAudioVM(666, "1", "Title", 1),
                new MBAudioVM(666, "Down MB Artist", "Title", 0),
            };
        }
    }
}