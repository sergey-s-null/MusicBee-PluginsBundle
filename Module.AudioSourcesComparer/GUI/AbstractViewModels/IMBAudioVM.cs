namespace Module.AudioSourcesComparer.GUI.AbstractViewModels
{
    public interface IMBAudioVM
    {
        int VkId { get; }
        int Index { get; }
        string Artist { get; }
        string Title { get; }
        
        // todo copy commands
    }
}