﻿using Module.MusicSourcesStorage.Logic.Entities.EventArgs;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public abstract class MultiStepTaskWrapperBase<TResult> : IMultiStepTask<TResult>
{
    public event EventHandler<StepProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<StepFailedEventArgs>? Failed;
    public event EventHandler<StepCancelledEventArgs>? Cancelled;
    public event EventHandler<StepSuccessfullyCompletedEventArgs>? StepSuccessfullyCompleted;
    public event EventHandler<TaskResultEventArgs<TResult>>? FullySuccessfullyCompleted;

    public abstract int StepCount { get; }

    public abstract bool IsActivated { get; }

    public abstract Task<TResult> Task { get; }

    protected void DispatchEventsToSimulateFullCompletion(TResult result)
    {
        for (var i = 0; i < StepCount; i++)
        {
            ProgressChanged?.Invoke(
                this,
                new StepProgressChangedEventArgs(i, 1)
            );
            StepSuccessfullyCompleted?.Invoke(
                this,
                new StepSuccessfullyCompletedEventArgs(i)
            );
        }

        FullySuccessfullyCompleted?.Invoke(
            this,
            new TaskResultEventArgs<TResult>(result)
        );
    }

    protected void InitializeEventsForFinalTask(ITask<TResult> task, int stepIndex)
    {
        InitializeCommonEvents(task, stepIndex);
        task.SuccessfullyCompleted += (_, args) =>
        {
            StepSuccessfullyCompleted?.Invoke(
                this,
                new StepSuccessfullyCompletedEventArgs(stepIndex)
            );
            FullySuccessfullyCompleted?.Invoke(
                this,
                new TaskResultEventArgs<TResult>(args.Result)
            );
        };
    }

    protected void InitializeEvents<T>(IMultiStepTask<T> task, int stepOffset)
    {
        InitializeCommonEvents(task, stepOffset);
    }

    protected void InitializeEventsForFinalTask(IMultiStepTask<TResult> task, int stepOffset)
    {
        InitializeCommonEvents(task, stepOffset);
        task.FullySuccessfullyCompleted += (_, args) => FullySuccessfullyCompleted?.Invoke(
            this,
            args
        );
    }

    protected void InitializeEventsForFinalTaskWithDifferentResult<TDifferentResult>(
        IMultiStepTask<TDifferentResult> task,
        Func<TDifferentResult, TResult> resultSelector,
        int stepOffset)
    {
        InitializeCommonEvents(task, stepOffset);
        task.FullySuccessfullyCompleted += (_, args) => FullySuccessfullyCompleted?.Invoke(
            this,
            new TaskResultEventArgs<TResult>(resultSelector(args.Result))
        );
    }

    private void InitializeCommonEvents<T>(ITask<T> task, int stepIndex)
    {
        task.ProgressChanged += (_, args) =>
            ProgressChanged?.Invoke(
                this,
                new StepProgressChangedEventArgs(stepIndex, args.Progress)
            );
        task.Failed += (_, args) => Failed?.Invoke(this, new StepFailedEventArgs(stepIndex, args.Exception));
        task.Cancelled += (_, _) => Cancelled?.Invoke(this, new StepCancelledEventArgs(stepIndex));
    }

    private void InitializeCommonEvents<T>(IMultiStepTask<T> task, int stepOffset)
    {
        task.ProgressChanged += (_, args) => ProgressChanged?.Invoke(
            this,
            new StepProgressChangedEventArgs(args.StepIndex + stepOffset, args.Progress)
        );
        task.Failed += (_, args) => Failed?.Invoke(
            this,
            new StepFailedEventArgs(args.StepIndex + stepOffset, args.Exception)
        );
        task.Cancelled += (_, args) => Cancelled?.Invoke(
            this,
            new StepCancelledEventArgs(args.StepIndex + stepOffset)
        );
        task.StepSuccessfullyCompleted += (_, args) => StepSuccessfullyCompleted?.Invoke(
            this,
            new StepSuccessfullyCompletedEventArgs(args.StepIndex + stepOffset)
        );
    }
}