using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableTaskWithTokenWrapper<TArgs, TResult> :
    TaskWrapperBase<TResult>,
    IActivableWithoutCancellationTaskWithProgress<TArgs, TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TArgs, TResult> _internalTask;
    private readonly CancellationToken _token;

    public ActivableTaskWithTokenWrapper(
        IActivableTaskWithProgress<TArgs, TResult> internalTask,
        CancellationToken token)
    {
        _internalTask = internalTask;
        _token = token;

        InitializeEvents(_internalTask);
    }

    public void Activate(TArgs args)
    {
        _internalTask.Activate(args, _token);
    }
}