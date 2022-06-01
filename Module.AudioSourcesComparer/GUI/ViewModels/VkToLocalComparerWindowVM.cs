using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Module.AudioSourcesComparer.DataClasses;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
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

        public VkToLocalComparerWindowVM(IVkToLocalComparerService vkToLocalComparerService)
        {
            _vkToLocalComparerService = vkToLocalComparerService;
        }

        private void Refresh()
        {
            var difference = _vkToLocalComparerService.FindDifferences();
            
            VkOnlyAudios.Clear();
            foreach (var vkAudio in difference.VkOnly)
            {
                VkOnlyAudios.Add(MapVkAudio(vkAudio));
            }
            
            LocalOnlyAudios.Clear();
            foreach (var mbAudio in difference.MBOnly)
            {
                LocalOnlyAudios.Add(MapMBAudio(mbAudio));
            }
        }

        private IVkAudioVM MapVkAudio(VkAudio vkAudio)
        {
            return new VkAudioVM(vkAudio.Id, vkAudio.Artist, vkAudio.Title);
        }

        private IMBAudioVM MapMBAudio(MBAudio mbAudio)
        {
            return new MBAudioVM(mbAudio.VkId, mbAudio.Index, mbAudio.Artist, mbAudio.Title);
        }

        private void DeleteAllVkOnlyAudios()
        {
            // todo
        }
    }
}