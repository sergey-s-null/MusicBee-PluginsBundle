using System.Collections.Generic;
using System.Windows.Input;

namespace Module.AudioSourcesComparer.GUI.AbstractViewModels
{
    public interface IVkToLocalComparerWindowVM
    {
        ICommand RefreshCmd { get; }
        
        IReadOnlyCollection<IVkAudioVM> VkOnlyAudios { get; }
        ICommand DeleteAllVkOnlyAudios { get; }
        
        IReadOnlyCollection<IMBAudioVM> LocalOnlyAudios { get; }
    }
}