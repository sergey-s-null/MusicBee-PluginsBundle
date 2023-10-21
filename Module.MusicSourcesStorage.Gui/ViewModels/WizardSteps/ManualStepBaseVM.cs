using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.Mvvm.Extension;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public abstract class ManualStepBaseVM : IManualWizardStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public abstract bool CanSafelyCloseWizard { get; }

    public abstract bool HasNextStep { get; }
    public abstract bool CanGoNext { get; }
    public abstract string? CustomNextStepName { get; }

    public abstract bool HasPreviousStep { get; }
    public abstract bool CanGoBack { get; }

    public abstract string? CustomCloseWizardCommandName { get; }

    public ICommand Next => _nextCmd ??= new RelayCommand(NextCmd);
    public ICommand Back => _backCmd ??= new RelayCommand(BackCmd);
    public ICommand CloseWizard => _closeWizardCmd ??= new RelayCommand(CloseWizardCmd);

    private ICommand? _nextCmd;
    private ICommand? _backCmd;
    private ICommand? _closeWizardCmd;

    protected abstract IWizardStepVM GetNextStep();

    protected abstract IWizardStepVM GetPreviousStep();

    private void NextCmd()
    {
        if (!HasNextStep)
        {
            throw new InvalidOperationException("Next step does not exists.");
        }

        if (!CanGoNext)
        {
            throw new InvalidOperationException("Can't go next.");
        }

        StepTransitionRequested?.Invoke(
            this,
            new StepTransitionEventArgs(GetNextStep())
        );
    }

    private void BackCmd()
    {
        if (!HasPreviousStep)
        {
            throw new InvalidOperationException("Previous step does not exists.");
        }

        if (!CanGoBack)
        {
            throw new InvalidCastException("Can't got back.");
        }

        StepTransitionRequested?.Invoke(
            this,
            new StepTransitionEventArgs(GetPreviousStep())
        );
    }

    private void CloseWizardCmd()
    {
        CloseWizardRequested?.Invoke(this, EventArgs.Empty);
    }
}