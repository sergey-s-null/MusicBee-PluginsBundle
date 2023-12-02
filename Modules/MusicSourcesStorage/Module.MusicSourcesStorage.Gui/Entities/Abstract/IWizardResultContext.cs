namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IWizardResultContext<T>
{
    T? Result { get; set; }
}