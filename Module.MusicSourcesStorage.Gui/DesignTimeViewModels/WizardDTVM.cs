using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class WizardDTVM : IWizardVM
{
    public IWizardStepVM CurrentStep { get; }

    public bool HasNextStep => true;
    public string? CustomNextCommandName => "Add";

    public bool HasPreviousStep => true;

    public string? CustomCloseWizardCommandName => "End";

    public ICommand Next => null!;
    public ICommand Back => null!;
    public ICommand Close => null!;

    public WizardDTVM()
    {
        CurrentStep = new ProcessingStepDTVM();
    }

    public WizardDTVM(IWizardStepVM initialStep)
    {
        CurrentStep = initialStep;
    }

    public WizardDTVM(IWizardStepDescriptor descriptor)
    {
        CurrentStep = descriptor.CreateStepViewModel();
    }
}