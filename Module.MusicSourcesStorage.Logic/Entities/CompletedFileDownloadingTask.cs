using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class CompletedFileDownloadingTask : IFileDownloadingTask
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskCompletedEventArgs>? Completed;

    public TaskState State => TaskState.Completed;

    public string TargetFilePath { get; }

    public CompletedFileDownloadingTask(string downloadedFilePath)
    {
        TargetFilePath = downloadedFilePath;
    }

    public void WaitCompletion()
    {
    }

    public Task WaitCompletionAsync(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
}