using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Module.AudioSourcesComparer.DataClasses;
using Module.AudioSourcesComparer.Exceptions;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.AudioSourcesComparer.GUI.Factories;
using Module.AudioSourcesComparer.Services.Abstract;
using Module.Vk.Settings;
using PropertyChanged;
using Root.Helpers;
using Root.MVVM;
using VkNet.Abstractions;
using VkNet.Exception;

namespace Module.AudioSourcesComparer.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VkToLocalComparerWindowVM : IVkToLocalComparerWindowVM
    {
        public ICommand RefreshCmd => _refreshCmd ??= new RelayCommand(async _ => await RefreshAsync());
        private ICommand? _refreshCmd;
        public bool Refreshing { get; private set; }

        public IList<IVkAudioVM> VkOnlyAudios { get; } = new ObservableCollection<IVkAudioVM>();

        public ICommand DeleteAllVkOnlyAudiosCmd =>
            _deleteAllVkOnlyAudiosCmd ??= new RelayCommand(_ => DeleteAllVkOnlyAudios());

        private ICommand? _deleteAllVkOnlyAudiosCmd;

        public IList<IMBAudioVM> LocalOnlyAudios { get; } = new ObservableCollection<IMBAudioVM>();

        private readonly IVkToLocalComparerService _vkToLocalComparerService;
        private readonly IVkAudioVMFactory _vkAudioVMFactory;
        private readonly IVkApi _vkApi;
        private readonly IVkSettings _vkSettings;

        public VkToLocalComparerWindowVM(
            IVkToLocalComparerService vkToLocalComparerService,
            IVkAudioVMFactory vkAudioVMFactory,
            IVkApi vkApi,
            IVkSettings vkSettings)
        {
            _vkToLocalComparerService = vkToLocalComparerService;
            _vkAudioVMFactory = vkAudioVMFactory;
            _vkApi = vkApi;
            _vkSettings = vkSettings;
        }

        private async Task RefreshAsync()
        {
            if (Refreshing)
            {
                return;
            }

            Refreshing = true;

            VkOnlyAudios.Clear();
            LocalOnlyAudios.Clear();

            var difference = await FindDifferencesAndShowMessageOnErrorAsync();
            if (difference is null)
            {
                return;
            }

            foreach (var vkAudio in difference!.VkOnly)
            {
                var vkAudioVM = MapVkAudio(vkAudio);
                vkAudioVM.DeleteRequested += (_, _) => VkOnlyAudios.Remove(vkAudioVM);
                VkOnlyAudios.Add(vkAudioVM);
            }

            foreach (var mbAudio in difference.MBOnly)
            {
                LocalOnlyAudios.Add(MapMBAudio(mbAudio));
            }

            Refreshing = false;
        }

        private async Task<AudiosDifference?> FindDifferencesAndShowMessageOnErrorAsync()
        {
            try
            {
                return await _vkToLocalComparerService.FindDifferencesAsync();
            }
            catch (VkApiUnauthorizedException e)
            {
                MessageBox.Show(
                    "Vk api seems to be unauthorized.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                return null;
            }
            catch (VkApiInvalidValueException e)
            {
                MessageBox.Show(
                    "Got invalid value from vk api.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                return null;
            }
            catch (MBApiException e)
            {
                MessageBox.Show(
                    "Music bee api error.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                return null;
            }
            catch (MBLibraryInvalidStateException e)
            {
                MessageBox.Show(
                    "Music bee library invalid state.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                return null;
            }
        }

        private IVkAudioVM MapVkAudio(VkAudio vkAudio)
        {
            return _vkAudioVMFactory.Create(vkAudio.Id, vkAudio.Artist, vkAudio.Title);
        }

        private static IMBAudioVM MapMBAudio(MBAudio mbAudio)
        {
            return new MBAudioVM(mbAudio.FilePath, mbAudio.VkId, mbAudio.Index, mbAudio.Artist, mbAudio.Title);
        }

        private void DeleteAllVkOnlyAudios()
        {
            var vkOnlyAudiosCopy = VkOnlyAudios.ToReadOnlyCollection();

            foreach (var vkAudioVM in vkOnlyAudiosCopy)
            {
                if (TryDeleteVkAudioFromVk(vkAudioVM))
                {
                    VkOnlyAudios.Remove(vkAudioVM);
                }
            }
        }

        private bool TryDeleteVkAudioFromVk(IVkAudioVM vkAudioVM)
        {
            try
            {
                var deleted = _vkApi.Audio.Delete(vkAudioVM.Id, _vkSettings.UserId);
                return deleted;
            }
            catch (VkApiException)
            {
                return false;
            }
        }
    }
}