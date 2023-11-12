﻿using Module.MusicSourcesStorage.Logic.Entities.EventArgs;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface ITaskWithProgress<TResult>
{
    event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    event EventHandler<TaskFailedEventArgs> Failed;
    event EventHandler Cancelled;
    event EventHandler<TaskResultEventArgs<TResult>> SuccessfullyCompleted;

    bool IsActivated { get; }

    /// <exception cref="InvalidOperationException">Task is not activated.</exception>
    Task<TResult> Task { get; }
}