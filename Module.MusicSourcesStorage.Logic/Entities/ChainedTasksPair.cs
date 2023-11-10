using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ChainedTasksPair<TFirstResult, TResult> : IMultiStepTaskWithProgress<TResult>
{
    public event EventHandler<StepProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<StepFailedEventArgs>? Failed;
    public event EventHandler<StepCancelledEventArgs>? Cancelled;
    public event EventHandler<StepSuccessfullyCompletedEventArgs>? StepSuccessfullyCompleted;
    public event EventHandler<TaskResultEventArgs<TResult>>? FullySuccessfullyCompleted;

    public int StepCount => 2;

    public Task<TResult> Task { get; }

    private readonly Func<ITaskWithProgress<TFirstResult>> _firstTaskFactory;
    private readonly Func<TFirstResult, ITaskWithProgress<TResult>> _secondTaskFactory;

    public ChainedTasksPair(
        ITaskWithProgress<TFirstResult> firstTask,
        Func<TFirstResult, ITaskWithProgress<TResult>> secondTaskFactory)
        : this(() => firstTask, secondTaskFactory)
    {
    }

    public ChainedTasksPair(
        Func<ITaskWithProgress<TFirstResult>> firstTaskFactory,
        Func<TFirstResult, ITaskWithProgress<TResult>> secondTaskFactory)
    {
        _firstTaskFactory = firstTaskFactory;
        _secondTaskFactory = secondTaskFactory;

        Task = new Task<TResult>(Execute);
    }

    public void Activate()
    {
        if (Task.Status != TaskStatus.Created)
        {
            return;
        }

        Task.Start();
    }

    private TResult Execute()
    {
        var firstTask = _firstTaskFactory();
        InitializeEvents(firstTask, 0);
        firstTask.Activate();

        var firstResult = firstTask.Task.Result;

        var secondTask = _secondTaskFactory(firstResult);
        InitializeEvents(secondTask, 1);
        secondTask.Activate();

        var result = secondTask.Task.Result;
        FullySuccessfullyCompleted?.Invoke(this, new TaskResultEventArgs<TResult>(result));
        return result;
    }

    private void InitializeEvents<T>(ITaskWithProgress<T> task, int stepIndex)
    {
        task.ProgressChanged += (_, args) =>
            ProgressChanged?.Invoke(
                this,
                new StepProgressChangedEventArgs(stepIndex, args.Progress)
            );
        task.Failed += (_, args) => Failed?.Invoke(this, new StepFailedEventArgs(stepIndex, args.Exception));
        task.Cancelled += (_, _) => Cancelled?.Invoke(this, new StepCancelledEventArgs(stepIndex));
        task.SuccessfullyCompleted += (_, _) =>
            StepSuccessfullyCompleted?.Invoke(
                this,
                new StepSuccessfullyCompletedEventArgs(stepIndex)
            );
    }
}