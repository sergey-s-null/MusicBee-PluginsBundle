using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.EventArgs;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Commands;

public sealed class DownloadFileCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    // todo is this useful?
    public event EventHandler<ProcessingStateChangedEventArgs>? ProcessingStateChanged;
    public event EventHandler? Downloaded;

    private readonly int _fileId;
    private readonly IFileOperationLocker _fileOperationLocker;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFilesDownloadingService _filesDownloadingService;

    public DownloadFileCommand(
        int fileId,
        IFileOperationLocker fileOperationLocker,
        IFilesLocatingService filesLocatingService,
        IFilesDownloadingService filesDownloadingService)
    {
        _fileId = fileId;
        _fileOperationLocker = fileOperationLocker;
        _filesLocatingService = filesLocatingService;
        _filesDownloadingService = filesDownloadingService;

        _fileOperationLocker.LockStateChanged += OnLockStateChanged;
    }

    public bool CanExecute(object parameter)
    {
        // todo subscribe on existence event -> CanExecuteChanged
        return !_fileOperationLocker.IsLocked(_fileId)
               && _filesLocatingService.IsFileExistsAsync(_fileId).Result;
    }

    public void Execute(object parameter)
    {
        try
        {
            using var _ = _fileOperationLocker.Lock(_fileId, TimeSpan.Zero);

            RaiseProcessing();

            var task = _filesDownloadingService.CreateFileDownloadTaskAsync(_fileId).Result;
            task.Activated(new FileDownloadArgs(true)).Task.Wait();

            RaiseNotProcessing();
            RaiseDownloaded();
        }
        catch (TimeoutException)
        {
        }
    }

    private void RaiseProcessing()
    {
        ProcessingStateChanged?.Invoke(
            this,
            new ProcessingStateChangedEventArgs(true)
        );
    }

    private void RaiseNotProcessing()
    {
        ProcessingStateChanged?.Invoke(
            this,
            new ProcessingStateChangedEventArgs(false)
        );
    }

    private void RaiseDownloaded()
    {
        Downloaded?.Invoke(this, EventArgs.Empty);
    }

    private void OnLockStateChanged(object _, LockStateChangedEventArgs args)
    {
        if (args.FileId == _fileId)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}