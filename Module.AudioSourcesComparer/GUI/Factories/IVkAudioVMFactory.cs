using Module.AudioSourcesComparer.GUI.AbstractViewModels;

namespace Module.AudioSourcesComparer.GUI.Factories
{
    public interface IVkAudioVMFactory
    {
        IVkAudioVM Create(long id, string artist, string title);
    }
}