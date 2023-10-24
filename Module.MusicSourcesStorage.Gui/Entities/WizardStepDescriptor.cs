using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class WizardStepDescriptor : IWizardStepDescriptor
{
    public IWizardStepDescriptor? NextStepDescriptor { get; set; }
    public IWizardStepDescriptor? PreviousStepDescriptor { get; set; }
    public IWizardStepDescriptor? ErrorStepDescriptor { get; set; }

    public string? CustomNextCommandName { get; set; }
    public string? CustomCloseWizardCommandName { get; set; }
    public bool CanSafelyCloseWizard { get; set; }

    private readonly StepType _stepType;
    private readonly IWizardStepViewModelsFactory _viewModelsFactory;

    public WizardStepDescriptor(
        StepType stepType,
        IWizardStepViewModelsFactory viewModelsFactory)
    {
        _stepType = stepType;
        _viewModelsFactory = viewModelsFactory;
    }

    public IWizardStepVM CreateStepViewModel()
    {
        return _viewModelsFactory.Create(_stepType);
    }
}