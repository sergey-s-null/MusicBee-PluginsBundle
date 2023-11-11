﻿using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithChangedArgsWrapper<TArgs, TResult, TChangedArgs> :
    TaskWrapperBase<TResult>,
    IActivableTaskWithProgress<TChangedArgs, TResult>
{
    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TArgs, TResult> _internalTask;
    private readonly Func<TChangedArgs, TArgs> _oldArgsSelector;

    public ActivableTaskWithChangedArgsWrapper(
        IActivableTaskWithProgress<TArgs, TResult> internalTask,
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