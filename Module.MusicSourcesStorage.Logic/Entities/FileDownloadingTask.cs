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

    private readonly string _sourceUri;
    private readonly string _targetFilePath;
    private readonly bool _createDirectory;
    private CancellationToken _token;

    public FileDownloadingTask(
        string sourceUri,
        string targetFilePath,
        bool createDirectory,
        CancellationToken token)
    {
        _sourceUri = sourceUri;
        _targetFilePath = targetFilePath;
        _createDirectory = createDirectory;
        _token = token;

        Task = new Task<string>(ExecuteDownloading, token);
    }

    public void Activate()
    {
        if (Task.Status != TaskStatus.Created)
        {
            return;
        }

        Task.Start();
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
        _token.Register(webClient.CancelAsync);
        webClient.DownloadProgressChanged += (_, args) => ProgressChanged?.Invoke(
            this,
            new ProgressChangedEventArgs(args.ProgressPercentage / 100.0)
        );

        webClient.DownloadFile(_sourceUri, _targetFilePath);
    }
}