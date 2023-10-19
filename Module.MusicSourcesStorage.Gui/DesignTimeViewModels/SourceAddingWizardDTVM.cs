using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class SourceAddingWizardDTVM : ISourceAddingWizardVM
{
    public ISourceAddingWizardStepVM CurrentStep { get; private set; }

    public ICommand Back => _backCmd ??= new RelayCommand(BackCmd);
    public ICommand Next => _nextCmd ??= new RelayCommand(NextCmd);
    public ICommand Cancel => _cancelCmd ??= new RelayCommand(CancelCmd);

    private ICommand? _backCmd;
    private ICommand? _nextCmd;
    private ICommand? _cancelCmd;

    public SourceAddingWizardDTVM(ISourceAddingWizardStepVM initialStep)
    {
        CurrentStep = initialStep;
    }

    private void BackCmd()
    {
        if (!CurrentStep.CanGoBack)
        {
            return;
        }

        CurrentStep = CurrentStep.GoBack();
    }

    private void NextCmd()
    {
        if (!CurrentStep.CanGoNext)
        {
            // todo close wizard
            return;
        }

        CurrentStep = CurrentStep.GoNext();
    }

    private void CancelCmd()
    {
        // todo close wizard
    }
}