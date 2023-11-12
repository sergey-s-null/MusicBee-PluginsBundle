using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableTaskWithTokenWrapper<TResult> :
    TaskWrapperBase<TResult>,
    IActivableWithoutCancellationTaskWithProgress<TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TResult> _internalTask;
    private readonly CancellationToken _token;

    public ActivableTaskWithTokenWrapper(
        IActivableTaskWithProgress<TResult> internalTask,
        CancellationToken token)
    {
        _internalTask = internalTask;
        _token = token;

        InitializeEvents(_internalTask);
    }

    public void Activate()
    {
        _internalTask.Activate(_token);
    }
}