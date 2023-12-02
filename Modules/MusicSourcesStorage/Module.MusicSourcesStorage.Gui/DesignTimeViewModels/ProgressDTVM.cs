using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class ProgressDTVM : IProgressVM
{
    public int StepNumber => 2;
    public int StepCount => 3;

    public int Percentage => 65;
}