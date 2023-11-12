using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableTaskWithArgsWrapper<TArgs, TResult> :
    TaskWrapperBase<TResult>,
    IActivableTaskWithProgress<TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TArgs, TResult> _internalTask;
    private readonly TArgs _args;

    public ActivableTaskWithArgsWrapper(
        IActivableTaskWithProgress<TArgs, TResult> internalTask,
        TArgs args)
    {
        _internalTask = internalTask;
        _args = args;

        InitializeEvents(_internalTask);
    }

    public void Activate(CancellationToken token)
    {
        _internalTask.Activate(_args, token);
    }
}