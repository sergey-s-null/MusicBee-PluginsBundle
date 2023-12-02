using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Views;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class WizardVM : IWizardVM
{
    public IWizardStepVM CurrentStep { get; private set; }

    [DependsOn(nameof(CurrentStep))]
    public bool HasNextStep =>
        _currentStepDescriptor.NextStepDescriptor is not null;

    [DependsOn(nameof(CurrentStep))]
    public string? CustomNextCommandName =>
        _currentStepDescriptor.CustomNextCommandName;

    [DependsOn(nameof(CurrentStep))]
    public bool HasPreviousStep =>
        _currentStepDescriptor.PreviousStepDescriptor is not null;

    [DependsOn(nameof(CurrentStep))]
    public string? CustomCloseWizardCommandName =>
        _currentStepDescriptor.CustomCloseWizardCommandName;

    public ICommand Next => _nextCmd ??= new RelayCommand(NextCmd);
    public ICommand Back => _backCmd ??= new RelayCommand(BackCmd);
    public ICommand Close => _closeCmd ??= new RelayCommand(CloseCmd);

    private ICommand? _nextCmd;
    private ICommand? _backCmd;
    private ICommand? _closeCmd;

    private readonly object _stepTransitionSync = new();
    private readonly Wizard _wizard;
    private readonly IWizardStepVMFactory _stepVMFactory;

    private IWizardStepDescriptor _currentStepDescriptor;

    public WizardVM(
        Wizard wizard,
        IWizardStepDescriptor initialStepDescriptor,
        IWizardStepVMFactory stepVMFactory)
    {
        _wizard = wizard;
        _stepVMFactory = stepVMFactory;

        (_currentStepDescriptor, CurrentStep) = (null!, null!);
        GoToStep(initialStepDescriptor);
    }

    private void ActivateStep(IWizardStepVM step)
    {
        if (step is IAutomaticWizardStepVM automaticStep)
        {
            automaticStep.ProcessingCompleted += OnAutomaticStepProcessed;
            automaticStep.Start();
        }
    }

    private void DeactivateStep(IWizardStepVM step)
    {
        if (step is IAutomaticWizardStepVM automaticStep)
        {
            automaticStep.ProcessingCompleted -= OnAutomaticStepProcessed;
        }
    }

    private void OnAutomaticStepProcessed(object sender, StepResultEventArgs args)
    {
        ProcessAutomaticStepResult(args.Result);
    }

    private void NextCmd()
    {
        if (CurrentStep is not IManualWizardStepVM manualStep)
        {
            throw new InvalidOperationException(
                "Can't go next. Current step is not manual step."
            );
        }

        if (!manualStep.IsValidState)
        {
            throw new InvalidOperationException(
                "Can't got next. Current step has invalid state."
            );
        }

        var result = manualStep.Confirm();
        ProcessManualStepResult(result);
    }

    private void BackCmd()
    {
        if (CurrentStep is not IManualWizardStepVM)
        {
            throw new InvalidOperationException(
                "Can't go back. Current step is not manual step."
            );
        }

        GoBack();
    }

    private void CloseCmd()
    {
        _wizard.Close(!_currentStepDescriptor.CanSafelyCloseWizard);
    }

    private void ProcessManualStepResult(StepResult result)
    {
        DeactivateStep(CurrentStep);

        switch (result)
        {
            case StepResult.Success:
                GoNext();
                break;
            case StepResult.Error:
                GoError();
                break;
            case StepResult.Canceled:
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(result),
                    result,
                    "Got unknown or unsupported step result."
                );
        }
    }

    private void ProcessAutomaticStepResult(StepResult result)
    {
        DeactivateStep(CurrentStep);

        switch (result)
        {
            case StepResult.Success:
                GoNextOrClose();
                break;
            case StepResult.Canceled:
                GoBackOrClose();
                break;
            case StepResult.Error:
                GoError();
                break;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(result),
                    result,
                    "Got unknown step result"
                );
        }
    }

    private void GoNext()
    {
        if (_currentStepDescriptor.NextStepDescriptor is null)
        {
            throw new InvalidOperationException(
                "Can't go next. Next step descriptor is empty."
            );
        }

        GoToStep(_currentStepDescriptor.NextStepDescriptor);
    }

    private void GoNextOrClose()
    {
        if (_currentStepDescriptor.NextStepDescriptor is null)
        {
            _wizard.Close(false);
            return;
        }

        GoToStep(_currentStepDescriptor.NextStepDescriptor);
    }

    private void GoBack()
    {
        if (_currentStepDescriptor.PreviousStepDescriptor is null)
        {
            throw new InvalidOperationException(
                "Can't go back. Previous step descriptor is empty."
            );
        }

        GoToStep(_currentStepDescriptor.PreviousStepDescriptor);
    }

    private void GoBackOrClose()
    {
        if (_currentStepDescriptor.PreviousStepDescriptor is null)
        {
            _wizard.Close(false);
            return;
        }

        GoToStep(_currentStepDescriptor.PreviousStepDescriptor);
    }

    private void GoError()
    {
        if (_currentStepDescriptor.ErrorStepDescriptor is null)
        {
            throw new InvalidOperationException(
                "Can't go to error. Error step descriptor is empty."
            );
        }

        GoToStep(_currentStepDescriptor.ErrorStepDescriptor);
    }

    private void GoToStep(IWizardStepDescriptor stepDescriptor)
    {
        lock (_stepTransitionSync)
        {
            _currentStepDescriptor = stepDescriptor;
            var step = _stepVMFactory.Create(_currentStepDescriptor);
            ActivateStep(step);
            CurrentStep = step;
        }
    }
}