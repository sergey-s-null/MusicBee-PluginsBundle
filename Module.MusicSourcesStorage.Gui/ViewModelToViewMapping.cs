using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Views.WizardSteps;

namespace Module.MusicSourcesStorage.Gui;

public static class ViewModelToViewMapping
{
    public static readonly IDictionary<Type, Func<FrameworkElement>> Map =
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(ISelectVkPostStepVM)] = () => new SelectVkPostStep(),
            [typeof(IProcessingStepVM)] = () => new ProcessingStep(),
            [typeof(IErrorStepVM)] = () => new ErrorStep(),
        };
}