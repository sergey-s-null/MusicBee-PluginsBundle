using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface IFileDownloadingTask
{
    event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    event EventHandler<TaskCompletedEventArgs> Completed;

    TaskState State { get; }

    string TargetFilePath { get; }

    void WaitCompletion();

    Task WaitCompletionAsync(CancellationToken token = default);
}