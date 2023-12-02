namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IProgressVM
{
    int StepNumber { get; }
    int StepCount { get; }

    int Percentage { get; }
}