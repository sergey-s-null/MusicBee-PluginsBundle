using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableTaskWithArgsWrapper<TArgs, TResult> :
    TaskWrapperBase<TResult>,
    IActivableTask<Void, TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTask<TArgs, TResult> _internalTask;
    private readonly TArgs _args;

    public ActivableTaskWithArgsWrapper(
        IActivableTask<TArgs, TResult> internalTask,
        TArgs args)
    {
        _internalTask = internalTask;
        _args = args;

        InitializeEvents(_internalTask);
    }

    public void Activate(Void _, CancellationToken token)
    {
        _internalTask.Activate(_args, token);
    }
}