using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class WizardStepDescriptor : IWizardStepDescriptor
{
    public IWizardStepDescriptor? NextStepDescriptor { get; set; }
    public IWizardStepDescriptor? PreviousStepDescriptor { get; set; }
    public IWizardStepDescriptor? ErrorStepDescriptor { get; set; }

    public string? CustomNextCommandName { get; set; }
    public string? CustomCloseWizardCommandName { get; set; }
    public bool CanSafelyCloseWizard { get; set; }

    private readonly Func<IWizardStepVM> _stepViewModelFactory;

    public WizardStepDescriptor(Func<IWizardStepVM> stepViewModelFactory)
    {
        _stepViewModelFactory = stepViewModelFactory;
    }

    public IWizardStepVM CreateStepViewModel()
    {
        return _stepViewModelFactory();
    }
}