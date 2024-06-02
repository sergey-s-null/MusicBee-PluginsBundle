using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Exceptions;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.Commands;

[AddINotifyPropertyChangedInterface]
public sealed class DownloadFileCommand : ICommand
{
    private readonly int _fileId;
    private readonly IFileOperationLocker _fileOperationLocker;
    private readonly IFilesDownloadingService _filesDownloadingService;

    public delegate DownloadFileCommand Factory(int fileId);

    public DownloadFileCommand(
        int fileId,
        IFileOperationLocker fileOperationLocker,
        IFilesDownloadingService filesDownloadingService)
    {
        _fileId = fileId;
        _fileOperationLocker = fileOperationLocker;
        _filesDownloadingService = filesDownloadingService;

        _fileOperationLocker.LockStateChanged += OnLockStateChanged;
    }

    public event EventHandler? CanExecuteChanged;
    public event EventHandler? Downloaded;

    public bool IsProcessing { get; private set; }

    public bool CanExecute(object parameter)
    {
        return !_fileOperationLocker.IsLocked(_fileId);
    }

    public async void Execute(object parameter)
    {
        try
        {
            using var _ = _fileOperationLocker.Lock(_fileId, TimeSpan.Zero);

            IsProcessing = true;

            var task = await _filesDownloadingService.CreateFileDownloadTaskAsync(_fileId);
            await task.Activated(new FileDownloadArgs(true)).Task;

            IsProcessing = false;
            RaiseDownloaded();
        }
        catch (LockTimeoutException)
        {
        }
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