using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class WizardDTVM : IWizardVM
{
    public IWizardStepVM CurrentStep { get; private set; }

    public ICommand Back => _backCmd ??= new RelayCommand(BackCmd);
    public ICommand Next => _nextCmd ??= new RelayCommand(NextCmd);
    public ICommand Cancel => _cancelCmd ??= new RelayCommand(CancelCmd);

    private ICommand? _backCmd;
    private ICommand? _nextCmd;
    private ICommand? _cancelCmd;

    public WizardDTVM()
    {
        CurrentStep = new ProcessingStepDTVM();
    }

    public WizardDTVM(IWizardStepVM initialStep)
    {
        CurrentStep = initialStep;
    }

    private void BackCmd()
    {
        throw new NotImplementedException();
    }

    private void NextCmd()
    {
        throw new NotImplementedException();
    }

    private void CancelCmd()
    {
        throw new NotImplementedException();
    }
}