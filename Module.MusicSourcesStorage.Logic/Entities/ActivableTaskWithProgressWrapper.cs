using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities.EventArgs;
using Module.MusicSourcesStorage.Logic.Extensions;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithProgressWrapper<TArgs, TResult> : IActivableTaskWithProgress<TArgs, TResult>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<TResult>>? SuccessfullyCompleted;

    public bool IsActivated { get; private set; }

    public Task<TResult> Task => IsActivated && _task is not null
        ? _task
        : throw new InvalidOperationException("Task is not activated.");

    private Task<TResult>? _task;

    private readonly Func<TArgs, CancellationToken, IActivableWithoutCancellationTaskWithProgress<TResult>>
        _internalTaskProvider;

    public ActivableTaskWithProgressWrapper(
        Func<TArgs, CancellationToken, IActivableWithoutCancellationTaskWithProgress<TResult>> internalTaskProvider)
    {
        _internalTaskProvider = internalTaskProvider;
    }

    public void Activate(TArgs args, CancellationToken token)
    {
        if (IsActivated || _task is not null)
        {
            throw new InvalidOperationException("Task already activated.");
        }

        var internalTask = _internalTaskProvider(args, token)
            .With(InitializeEvents)
            .Activated();

        _task = System.Threading.Tasks.Task.Run(
            () => internalTask.Task.Result,
            token
        );
        token.Register(() => Cancelled?.Invoke(this, System.EventArgs.Empty));
        IsActivated = true;
    }

    private void InitializeEvents(ITaskWithProgress<TResult> internalTask)
    {
        internalTask.ProgressChanged += (_, args) => ProgressChanged?.Invoke(this, args);
        internalTask.Failed += (_, args) => Failed?.Invoke(this, args);
        internalTask.Cancelled += (_, args) => Cancelled?.Invoke(this, args);
        internalTask.SuccessfullyCompleted += (_, args) => SuccessfullyCompleted?.Invoke(this, args);
    }
}