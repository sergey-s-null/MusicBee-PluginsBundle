using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Views.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.ViewMappers;

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
            [typeof(ISelectDocumentFromVkPostStepVM)] = () => new SelectDocumentFromVkPostStep(),
            [typeof(IIndexingResultStepVM)] = () => new IndexingResultStep(),
            [typeof(IEditMusicSourceAdditionalInfoStepVM)] = () => new EditMusicSourceAdditionalInfoStep(),
            [typeof(ISuccessResultStepVM)] = () => new SuccessResultStep(),
        }
    };

    private WizardStepsViewMapper()
    {
    }
}