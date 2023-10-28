using System.Windows.Input;
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

    public ICommand Cancel => _cancelCmd ??= new RelayCommand(CancelCmd);

    private ICommand? _cancelCmd;

    private readonly IAddingVkPostWithArchiveContext _context;

    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _task;

    protected ProcessingStepBaseVM(IAddingVkPostWithArchiveContext context)
    {
        _context = context;
    }

    public void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _task = ProcessAsync(_cancellationTokenSource.Token)
            .ContinueWith(HandleTaskEnd, _cancellationTokenSource.Token);
    }

    protected abstract Task ProcessAsync(CancellationToken token);

    private void HandleTaskEnd(Task task)
    {
        switch (task)
        {
            case { Status: TaskStatus.RanToCompletion }:
                DispatchProcessingCompletedEvent(StepResult.Success);
                break;
            case { Exception: not null }:
                _context.Error = task.Exception.ToString();
                DispatchProcessingCompletedEvent(StepResult.Error);
                break;
            default:
                _context.Error = "Task was not ran to completion and does not contains exception. " +
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