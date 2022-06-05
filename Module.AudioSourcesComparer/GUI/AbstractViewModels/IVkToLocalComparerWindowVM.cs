using System.Collections.Generic;
using System.Windows.Input;

namespace Module.AudioSourcesComparer.GUI.AbstractViewModels
{
    public interface IVkToLocalComparerWindowVM
    {
        ICommand RefreshCmd { get; }

        IList<IVkAudioVM> VkOnlyAudios { get; }
        ICommand DeleteAllVkOnlyAudiosCmd { get; }

        IList<IMBAudioVM> LocalOnlyAudios { get; }
    }
}