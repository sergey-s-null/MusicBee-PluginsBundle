namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class FileDownloadingTaskWithMetaInfo
{
    public FileDownloadingTask Task { get; }
    public CancellationTokenSource TokenSource { get; }
    public int TaskRequestingCount => _taskRequestingCount;

    private int _taskRequestingCount;

    public FileDownloadingTaskWithMetaInfo(
        FileDownloadingTask task,
        CancellationTokenSource tokenSource,
        int taskRequestingInitCount)
    {
        Task = task;
        TokenSource = tokenSource;
        _taskRequestingCount = taskRequestingInitCount;
    }

    public void IncreaseTaskRequestingCount()
    {
        Interlocked.Increment(ref _taskRequestingCount);
    }

    public void DecreaseTaskRequestingCount()
    {
        Interlocked.Decrement(ref _taskRequestingCount);
    }
}