using Module.AudioSourcesComparer.GUI.AbstractViewModels;

namespace Module.AudioSourcesComparer.GUI.Factories
{
    public delegate IVkAudioVM VkAudioVMFactory(long id, string artist, string title);
}