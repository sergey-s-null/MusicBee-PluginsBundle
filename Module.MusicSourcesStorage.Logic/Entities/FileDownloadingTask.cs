using System.Net;
using System.Runtime.ExceptionServices;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class FileDownloadingTask : IFileDownloadingTask
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;

    public event EventHandler<TaskCompletedEventArgs>? Completed;

    public TaskState State { get; private set; }

    public string TargetFilePath { get; }

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private readonly string _sourceUri;
    private readonly bool _createDirectory;

    private WebClient? _webClient;
    private Task? _downloadTask;

    public FileDownloadingTask(
        string sourceUri,
        string targetFilePath,
        bool createDirectory)
    {
        State = TaskState.Created;
        TargetFilePath = targetFilePath;

        _sourceUri = sourceUri;
        _createDirectory = createDirectory;
    }

    public void Start()
    {
        CheckThatNotStarted();

        CreateDirectoryIfNeeded();

        _webClient = new WebClient();
        _cancellationTokenSource.Token.Register(_webClient.CancelAsync);
        _webClient.DownloadProgressChanged += (_, args) => ProgressChanged?.Invoke(
            this,
            new ProgressChangedEventArgs(args.ProgressPercentage / 100.0)
        );

        _downloadTask = _webClient.DownloadFileTaskAsync(_sourceUri, TargetFilePath)
            .ContinueWith(HandleDownloadingEnd, _cancellationTokenSource.Token);

        State = TaskState.Running;
    }

    public void WaitCompletion()
    {
        CheckThatStarted();

        _downloadTask!.Wait();
    }

    public Task WaitCompletionAsync(CancellationToken token)
    {
        CheckThatStarted();

        return Task.Run(() => _downloadTask!.Wait(token), token);
    }

    public void Cancel()
    {
        CheckThatStarted();

        _cancellationTokenSource.Cancel();

        try
        {
            _downloadTask!.Wait();
        }
        catch (AggregateException e)
        {
            switch (e.InnerExceptions.Count)
            {
                case 1:
                {
                    if (e.InnerException is not TaskCanceledException)
                    {
                        ExceptionDispatchInfo.Capture(e.InnerException!).Throw();
                    }

                    break;
                }
                case > 1:
                    throw;
            }
        }
        catch (TaskCanceledException)
        {
        }
    }

    private void CreateDirectoryIfNeeded()
    {
        if (!_createDirectory)
        {
            return;
        }

        var directory = Path.GetDirectoryName(TargetFilePath);
        if (directory is null)
        {
            return;
        }

        Directory.CreateDirectory(directory);
    }

    private void HandleDownloadingEnd(Task task)
    {
        switch (task)
        {
            case { Status: TaskStatus.RanToCompletion }:
                Completed?.Invoke(this, new TaskCompletedEventArgs(true));
                break;
            case { Status: TaskStatus.Faulted }:
                Completed?.Invoke(this, new TaskCompletedEventArgs(false, task.Exception));
                break;
            default:
                Completed?.Invoke(this, new TaskCompletedEventArgs(false));
                break;
        }

        State = TaskState.Completed;
    }

    private void CheckThatNotStarted()
    {
        if (_webClient is not null || _downloadTask is not null)
        {
            throw new InvalidOperationException("Task already started.");
        }
    }

    private void CheckThatStarted()
    {
        if (_webClient is null || _downloadTask is null)
        {
            throw new InvalidOperationException("Task is not started.");
        }
    }
}