using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableTask<TArgs, TResult> :
    TaskBase<TResult>,
    IActivableTask<TArgs, TResult>
{
    public override bool IsActivated => _isActivated;

    public override Task<TResult> Task => IsActivated && _task is not null
        ? _task
        : throw new InvalidOperationException("Task is not activated.");

    private bool _isActivated;
    private Task<TResult>? _task;
    private readonly TaskFunction<TArgs, TResult> _taskFunction;

    public ActivableTask(TaskFunction<TArgs, TResult> taskFunction)
    {
        _taskFunction = taskFunction;
    }

    public void Activate(TArgs args, CancellationToken token)
    {
        if (IsActivated || _task is not null)
        {
            throw new InvalidOperationException("Task already activated.");
        }

        _task = System.Threading.Tasks.Task.Run(
            () => _taskFunction(args, OnProgressChanged, token),
            token
        );
        // ReSharper disable once MethodSupportsCancellation
        _task.ContinueWith(OnTaskEnded);
        _isActivated = true;
    }
}