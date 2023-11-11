using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithToken<TResult> :
    TaskWrapperBase<TResult>,
    IActivableWithoutCancellationTaskWithProgress<TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TResult> _internalTask;
    private readonly CancellationToken _token;

    public ActivableTaskWithToken(
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