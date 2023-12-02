using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class TaskProgressVM<TResult> : IProgressVM
{
    public int StepNumber => 1;
    public int StepCount => 1;

    public int Percentage { get; private set; }

    public TaskProgressVM(ITask<TResult> task)
    {
        task.ProgressChanged += (_, args) => Percentage = (int)(args.Progress * 100);
        task.SuccessfullyCompleted += (_, _) => Percentage = 100;
    }
}