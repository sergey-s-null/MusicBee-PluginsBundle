using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Gui.Factories;

public static class ProgressVMFactory
{
    public static IProgressVM Create<TResult>(ITask<TResult> task)
    {
        return new TaskProgressVM<TResult>(task);
    }

    public static IProgressVM Create<TResult>(IMultiStepTask<TResult> task)
    {
        return new MultiStepTaskProgressVM<TResult>(task);
    }
}