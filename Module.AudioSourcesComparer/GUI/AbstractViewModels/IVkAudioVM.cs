using System.Windows.Input;

namespace Module.AudioSourcesComparer.GUI.AbstractViewModels
{
    public interface IVkAudioVM
    {
        long Id { get; } // todo mb long
        string Artist { get; }
        string Title { get; }

        ICommand DeleteCmd { get; }
        //todo copy... commands
    }
}