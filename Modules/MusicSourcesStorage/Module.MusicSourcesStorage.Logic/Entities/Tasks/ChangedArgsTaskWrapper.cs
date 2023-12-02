using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ChangedArgsTaskWrapper<TArgs, TResult, TChangedArgs> :
    TaskWrapperBase<TResult>,
    IActivableTask<TChangedArgs, TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTask<TArgs, TResult> _internalTask;
    private readonly Func<TChangedArgs, TArgs> _oldArgsSelector;

    public ChangedArgsTaskWrapper(
        IActivableTask<TArgs, TResult> internalTask,
        Func<TChangedArgs, TArgs> oldArgsSelector)
    {
        _internalTask = internalTask;
        _oldArgsSelector = oldArgsSelector;

        ValidateInternalTaskNotActivated();

        InitializeEvents(_internalTask);
    }

    public void Activate(TChangedArgs args, CancellationToken token)
    {
        _internalTask.Activate(_oldArgsSelector(args), token);
    }

    private void ValidateInternalTaskNotActivated()
    {
        if (_internalTask.IsActivated)
        {
            throw new InvalidOperationException("Could not wrap activated task.");
        }
    }
}