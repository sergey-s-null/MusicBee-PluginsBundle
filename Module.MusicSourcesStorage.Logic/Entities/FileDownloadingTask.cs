using System.Net;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class FileDownloadingTask : ITaskWithProgress<string>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<string>>? SuccessfullyCompleted;

    public Task<string> Task { get; }

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private readonly string _sourceUri;
    private readonly string _targetFilePath;
    private readonly bool _createDirectory;

    public FileDownloadingTask(
        string sourceUri,
        string targetFilePath,
        bool createDirectory)
    {
        _sourceUri = sourceUri;
        _targetFilePath = targetFilePath;
        _createDirectory = createDirectory;

        Task = new Task<string>(ExecuteDownloading, _cancellationTokenSource.Token);
    }

    public void Start()
    {
        if (Task.Status != TaskStatus.Created)
        {
            throw new InvalidOperationException("Task already started.");
        }

        Task.Start();
    }

    public void Cancel()
    {
        if (Task.Status == TaskStatus.Created)
        {
            throw new InvalidOperationException("Task is not started.");
        }

        _cancellationTokenSource.Cancel();

        try
        {
            Task.Wait();
        }
        catch (AggregateException e)
        {
            if (e.InnerExceptions.Count == 1 && e.InnerException is TaskCanceledException)
            {
                Cancelled?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                throw;
            }
        }
        catch (TaskCanceledException)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }

    private string ExecuteDownloading()
    {
        try
        {
            CreateDirectoryIfNeeded();
            DownloadFile();
        }
        catch (Exception e)
        {
            Failed?.Invoke(this, new TaskFailedEventArgs(e));
            throw;
        }

        SuccessfullyCompleted?.Invoke(this, new TaskResultEventArgs<string>(_targetFilePath));

        return _targetFilePath;
    }

    private void CreateDirectoryIfNeeded()
    {
        if (!_createDirectory)
        {
            return;
        }

        var directory = Path.GetDirectoryName(_targetFilePath);
        if (directory is null)
        {
            return;
        }

        Directory.CreateDirectory(directory);
    }

    private void DownloadFile()
    {
        using var webClient = new WebClient();
        _cancellationTokenSource.Token.Register(webClient.CancelAsync);
        webClient.DownloadProgressChanged += (_, args) => ProgressChanged?.Invoke(
            this,
            new ProgressChangedEventArgs(args.ProgressPercentage / 100.0)
        );

        webClient.DownloadFile(_sourceUri, _targetFilePath);
    }
}