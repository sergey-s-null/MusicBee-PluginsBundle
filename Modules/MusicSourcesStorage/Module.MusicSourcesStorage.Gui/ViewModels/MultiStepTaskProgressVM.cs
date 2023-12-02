using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class MultiStepTaskProgressVM<TResult> : IProgressVM
{
    public int StepNumber { get; private set; }
    public int StepCount { get; }

    public int Percentage { get; private set; }

    public MultiStepTaskProgressVM(IMultiStepTask<TResult> task)
    {
        StepNumber = 1;
        StepCount = task.StepCount;
        Percentage = 0;

        task.ProgressChanged += (_, args) =>
        {
            UpdateStepNumber(args.StepIndex);
            Percentage = (int)(args.Progress * 100);
        };
        task.StepSuccessfullyCompleted += (_, args) =>
        {
            UpdateStepNumber(args.StepIndex);
            Percentage = 100;
        };
    }

    private void UpdateStepNumber(int stepIndex)
    {
        if (stepIndex + 1 != StepNumber)
        {
            StepNumber = stepIndex + 1;
        }
    }
}