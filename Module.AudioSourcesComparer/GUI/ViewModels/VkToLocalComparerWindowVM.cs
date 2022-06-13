﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Module.AudioSourcesComparer.DataClasses;
using Module.AudioSourcesComparer.Exceptions;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.AudioSourcesComparer.GUI.Factories;
using Module.AudioSourcesComparer.Services.Abstract;
using PropertyChanged;
using Root.MVVM;

namespace Module.AudioSourcesComparer.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VkToLocalComparerWindowVM : IVkToLocalComparerWindowVM
    {
        public ICommand RefreshCmd => _refreshCmd ??= new RelayCommand(_ => Refresh());
        private ICommand? _refreshCmd;

        public IList<IVkAudioVM> VkOnlyAudios { get; } = new ObservableCollection<IVkAudioVM>();

        public ICommand DeleteAllVkOnlyAudiosCmd =>
            _deleteAllVkOnlyAudiosCmd ??= new RelayCommand(_ => DeleteAllVkOnlyAudios());

        private ICommand? _deleteAllVkOnlyAudiosCmd;

        public IList<IMBAudioVM> LocalOnlyAudios { get; } = new ObservableCollection<IMBAudioVM>();

        private readonly IVkToLocalComparerService _vkToLocalComparerService;
        private readonly IVkAudioVMFactory _vkAudioVMFactory;

        public VkToLocalComparerWindowVM(
            IVkToLocalComparerService vkToLocalComparerService,
            IVkAudioVMFactory vkAudioVMFactory)
        {
            _vkToLocalComparerService = vkToLocalComparerService;
            _vkAudioVMFactory = vkAudioVMFactory;
        }

        private void Refresh()
        {
            if (!TryFindDifferencesAndShowMessageOnError(out var difference))
            {
                return;
            }

            VkOnlyAudios.Clear();
            foreach (var vkAudio in difference!.VkOnly)
            {
                var vkAudioVM = MapVkAudio(vkAudio);
                vkAudioVM.DeleteRequested += (_, _) => VkOnlyAudios.Remove(vkAudioVM);
                VkOnlyAudios.Add(vkAudioVM);
            }

            LocalOnlyAudios.Clear();
            foreach (var mbAudio in difference.MBOnly)
            {
                LocalOnlyAudios.Add(MapMBAudio(mbAudio));
            }
        }

        private bool TryFindDifferencesAndShowMessageOnError(out AudiosDifference? difference)
        {
            try
            {
                difference = _vkToLocalComparerService.FindDifferences();
                return true;
            }
            catch (VkApiUnauthorizedException e)
            {
                MessageBox.Show(
                    "Vk api seems to be unauthorized.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                difference = null;
                return false;
            }
            catch (VkApiInvalidValueException e)
            {
                MessageBox.Show(
                    "Got invalid value from vk api.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                difference = null;
                return false;
            }
            catch (MBApiException e)
            {
                MessageBox.Show(
                    "Music bee api error.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                difference = null;
                return false;
            }
            catch (MBLibraryInvalidStateException e)
            {
                MessageBox.Show(
                    "Music bee library invalid state.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
                difference = null;
                return false;
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
            // todo
        }
    }
}