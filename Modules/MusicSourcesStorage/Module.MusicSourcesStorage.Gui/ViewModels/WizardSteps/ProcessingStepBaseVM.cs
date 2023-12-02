using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.Mvvm.Extension;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public abstract class ProcessingStepBaseVM : IProcessingStepVM
{
    public event EventHandler<StepResultEventArgs>? ProcessingCompleted;

    public abstract string Text { get; protected set; }
    public abstract IProgressVM? Progress { get; protected set; }

    public ICommand Cancel => _cancelCmd ??= new RelayCommand(CancelCmd);

    private ICommand? _cancelCmd;

    private readonly IWizardErrorContext _errorContext;

    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _task;

    protected ProcessingStepBaseVM(IWizardErrorContext errorContext)
    {
        _errorContext = errorContext;
    }

    public void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _task = ProcessAsync(_cancellationTokenSource.Token)
            .ContinueWith(HandleTaskEnd, _cancellationTokenSource.Token);
    }

    protected abstract Task<StepResult> ProcessAsync(CancellationToken token);

    protected virtual void OnCancelled()
    {
    }

    private void HandleTaskEnd(Task<StepResult> task)
    {
        switch (task)
        {
            case { Status: TaskStatus.RanToCompletion }:
                DispatchProcessingCompletedEvent(task.Result);
                break;
            case { Exception: not null }:
                _errorContext.Error = task.Exception.ToString();
                DispatchProcessingCompletedEvent(StepResult.Error);
                break;
            default:
                _errorContext.Error = "Task was not ran to completion and does not contains exception. " +
                                      $"Task status: {task.Status}.";
                DispatchProcessingCompletedEvent(StepResult.Error);
                break;
        }
    }

    private void CancelCmd()
    {
        if (_cancellationTokenSource is null)
        {
            return;
        }

        _cancellationTokenSource.Cancel();
        try
        {
            _task?.Wait();
        }
        catch
        {
            // ignored
        }

        OnCancelled();

        DispatchProcessingCompletedEvent(StepResult.Canceled);
    }

    private void DispatchProcessingCompletedEvent(StepResult result)
    {
        ProcessingCompleted?.Invoke(
            this,
            new StepResultEventArgs(result)
        );
    }
}