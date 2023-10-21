using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.ViewMappings;
using Module.MusicSourcesStorage.Gui.Views.WizardSteps;

namespace Module.MusicSourcesStorage.Gui;

// todo move to ViewMappers dir
public sealed class WizardStepsViewMapper : ViewMapperBase
{
    public static readonly IViewMapper Instance = new WizardStepsViewMapper();

    protected override IReadOnlyList<IReadOnlyDictionary<Type, Func<FrameworkElement>>> PrioritizedMaps { get; } = new[]
    {
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(ISelectVkPostStepVM)] = () => new SelectVkPostStep(),
            [typeof(IProcessingStepVM)] = () => new ProcessingStep(),
            [typeof(IErrorStepVM)] = () => new ErrorStep(),
            [typeof(ISelectVkPostAttachmentStepVM)] = () => new SelectVkPostAttachmentStep(),
        }
    };

    private WizardStepsViewMapper()
    {
    }
}