using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VkMusicDownloader.Ex;
using VkMusicDownloader.GUI;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.Attachments;

namespace Outer_Tests
{
    public class TestMainWindowVM : BaseViewModel, IMainWindowVM
    {
        #region Bindings

        private bool _isLoadingVkAudio = false;
        public bool IsLoadingVkAudio
        {
            get => _isLoadingVkAudio;
            set
            {
                _isLoadingVkAudio = value;
                NotifyPropChanged(nameof(IsLoadingVkAudio));
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                NotifyPropChanged(nameof(IsRefreshing));
            }
        }

        private RelayCommand _refreshCmd;
        public RelayCommand RefreshCmd
            => _refreshCmd ?? (_refreshCmd = new RelayCommand(_ => Refresh()));

        private RelayCommand _applyCheckStateToSelectedCommand;
        public RelayCommand ApplyCheckStateToSelectedCmd
            => _applyCheckStateToSelectedCommand ?? (_applyCheckStateToSelectedCommand = new RelayCommand(arg =>
            {
                object[] arr = (object[])arg;
                VkAudioVM @checked = (VkAudioVM)arr[0];
                IList<object> selectedObjects = (IList<object>)arr[1];
                IEnumerable<VkAudioVM> selected = selectedObjects
                    .Where(obj => obj is VkAudioVM)
                    .Select(obj => (VkAudioVM)obj);

                Aga(@checked, selected);
            }));

        private RelayCommand _applyCommand;
        public RelayCommand ApplyCommand
            => _applyCommand ?? (_applyCommand = new RelayCommand(_ => Apply()));

        private bool _isApplying = false;
        public bool IsApplying
        {
            get => _isApplying;
            set
            {
                _isApplying = value;
                NotifyPropChanged(nameof(IsApplying));
            }
        }

        private ObservableCollection<BaseAudioVM> _audios;
        public ObservableCollection<BaseAudioVM> Audios
            => _audios ?? (_audios = new ObservableCollection<BaseAudioVM>());


        #endregion

        private VkApi _vkApi;
        public VkApi VkApi
        {
            get => _vkApi;
            set => _vkApi = value;
        }

        public TestMainWindowVM()
        {
            // Random random = new Random();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();

            _vkApi = new VkApi(serviceCollection);

            string tokenFilePath = @"C:\Users\Sergey\Desktop\vk_aga\token.txt";
            string token = File.ReadAllText(tokenFilePath);

            _vkApi.Authorize(new ApiAuthParams()
            {
                AccessToken = token
            });

        }


        private void Refresh()
        {
            Random random = new Random(2445);
            Audios.Clear();

            for (int i = 0; i < 20; i++)
                Audios.Add(RandMBVM(random));

            int insideIndex = 0;
            foreach (Audio audio in _vkApi.Audio.GetIter())
            {
                if (audio.Id is null || insideIndex == 30)
                    break;

                bool isCorrapted = !IVkApiEx.ConvertToMp3(audio.Url.AbsoluteUri, out string mp3Url);
                Audios.Add(new VkAudioVM()
                {
                    Artist = audio.Artist,
                    Title = audio.Title,
                    InsideIndex = insideIndex++,
                    Url = mp3Url,
                    IsCorraptedUrl = isCorrapted,
                    VkId = (long)audio.Id
                });
            }
        }

        private void Aga(VkAudioVM @checked, IEnumerable<VkAudioVM> selected)
        {
            if (!selected.Contains(@checked))
                return;

            foreach (VkAudioVM audioVM in selected)
            {
                if (@checked != audioVM)
                    audioVM.IsSelected = @checked.IsSelected;
            }
        }

        private void Apply()
        {
            MessageBox.Show("apply");
        }

        private async Task ApplyDecorated()
        {
            MessageBox.Show("apply");
        }



        // generations for tests
        private BaseAudioVM RandVM(Random random)
        {
            if (RandBool(random))
                return RandMBVM(random);
            else
                return RandVkVM(random);
        }

        private MBAudioVM RandMBVM(Random random)
        {
            return new MBAudioVM()
            {
                Artist = "MB",
                Title = RandStr(random, 20),
                VkId = random.Next(0, int.MaxValue),
                Index = random.Next(0, 1000)
            };
        }

        private VkAudioVM RandVkVM(Random random)
        {
            return new VkAudioVM()
            {
                InsideIndex = random.Next(0, 1000),
                IsSelected = RandBool(random),
                Artist = RandStr(random, 10),
                Title = "VK",
                VkId = random.Next(0, int.MaxValue)
            };
        }

        private string RandStr(Random random, int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append((char)('a' + random.Next(0, 26)));
            }
            return builder.ToString();
        }

        private bool RandBool(Random random)
        {
            return random.Next(0, 2) == 0;
        }



    }
}
