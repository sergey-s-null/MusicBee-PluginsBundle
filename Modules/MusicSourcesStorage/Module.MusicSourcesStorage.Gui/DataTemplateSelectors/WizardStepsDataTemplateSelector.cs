using System.Windows;
using System.Windows.Controls;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Views.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.DataTemplateSelectors;

public sealed class WizardStepsDataTemplateSelector : DataTemplateSelector
{
    private static readonly DataTemplate EmptyTemplate = new();

    public override DataTemplate SelectTemplate(object? viewModel, DependencyObject container)
    {
        if (viewModel is null)
        {
            return EmptyTemplate;
        }

        var factory = new FrameworkElementFactory(GetViewType(viewModel));
        return new DataTemplate
        {
            VisualTree = factory
        };
    }

    private static Type GetViewType(object viewModel)
    {
        return viewModel switch
        {
            ISelectVkPostStepVM => typeof(SelectVkPostStep),
            ISelectTorrentFileStepVM => typeof(SelectTorrentFileStep),
            IProcessingStepVM => typeof(ProcessingStep),
            IErrorStepVM => typeof(ErrorStep),
            ISelectDocumentFromVkPostStepVM => typeof(SelectDocumentFromVkPostStep),
            IIndexingResultStepVM => typeof(IndexingResultStep),
            IEditMusicSourceAdditionalInfoStepVM => typeof(EditMusicSourceAdditionalInfoStep),
            ISuccessResultStepVM => typeof(SuccessResultStep),
            _ => throw new ArgumentOutOfRangeException(
                nameof(viewModel),
                viewModel,
                $@"Could not provider appropriate view for view model {viewModel}."
            )
        };
    }
}