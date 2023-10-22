using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.Mvvm.Extension;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public abstract class ProcessingStepBaseVM : IProcessingStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public abstract bool CanSafelyCloseWizard { get; protected set; }

    public abstract string Text { get; protected set; }

    public ICommand Cancel => _cancelCmd ??= new RelayCommand(CancelCmd);

    private ICommand? _cancelCmd;

    private readonly IWizardCommonStepsFactory _commonStepsFactory;

    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _task;

    protected ProcessingStepBaseVM(IWizardCommonStepsFactory commonStepsFactory)
    {
        _commonStepsFactory = commonStepsFactory;
    }

    public void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _task = ProcessAsync(_cancellationTokenSource.Token)
            .ContinueWith(HandleTaskEnd, _cancellationTokenSource.Token);
    }

    protected abstract Task<IWizardStepVM> ProcessAsync(CancellationToken token);

    private void HandleTaskEnd(Task<IWizardStepVM> task)
    {
        switch (task)
        {
            case { Status: TaskStatus.RanToCompletion }:
                MakeTransition(task.Result);
                break;
            case { Exception: not null }:
                MakeTransition(_commonStepsFactory.CreateErrorStep(task.Exception));
                break;
            default:
                MakeTransition(_commonStepsFactory.CreateErrorStep(
                    "Task was not ran to completion and does not contains exception. " +
                    $"Task status: {task.Status}."
                ));
                break;
        }
    }

    private void MakeTransition(IWizardStepVM nextStep)
    {
        StepTransitionRequested?.Invoke(
            this,
            new StepTransitionEventArgs(nextStep)
        );
    }

    private void CancelCmd()
    {
        if (_cancellationTokenSource is null || _task is null)
        {
            return;
        }

        _cancellationTokenSource.Cancel();
        _task.Wait();
    }
}