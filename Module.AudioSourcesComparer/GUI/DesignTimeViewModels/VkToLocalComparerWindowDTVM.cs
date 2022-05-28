using System.Collections.Generic;
using System.Windows.Input;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Root.MVVM;

namespace Module.AudioSourcesComparer.GUI.DesignTimeViewModels
{
    public class VkToLocalComparerWindowDTVM : IVkToLocalComparerWindowVM
    {
        public ICommand RefreshCmd => new RelayCommand(_ => { });

        public IReadOnlyCollection<IVkAudioVM> VkOnlyAudios { get; } = new[]
        {
            new VkAudioDTVM(1, "Artist1", "Halo"),
            new VkAudioDTVM(2, "veryveryveryverylongcArtist1", "Halo"),
            new VkAudioDTVM(3, "art333", "veryveryveryrryryryryry long song name"),
            new VkAudioDTVM(4, "Aga", "chto"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
            new VkAudioDTVM(9999, "Repeat", "And"),
        };
        public ICommand DeleteAllVkOnlyAudios => new RelayCommand(_ => { });

        public IReadOnlyCollection<IMBAudioVM> LocalOnlyAudios { get; } = new IMBAudioVM[]
        {
            new MBAudioDTVM(667, 1, "No", "One"),
            new MBAudioDTVM(668, 2, "Ahahah", "Title"),
            new MBAudioDTVM(669, 3, "Love", "Death"),
            new MBAudioDTVM(300, 4, "Poly", "nothing"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
            new MBAudioDTVM(8885858, 9999, "Repeat", "And again"),
        };
    }
}