using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithArgs<TArgs, TResult> :
    TaskWrapperBase<TResult>,
    IActivableTaskWithProgress<TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TArgs, TResult> _internalTask;
    private readonly TArgs _args;

    public ActivableTaskWithArgs(
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