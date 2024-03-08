using System.Windows;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Commands;

public sealed class DeleteFileCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly int _fileId;
    private readonly string _filePath;
    private readonly bool _askBeforeDelete;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFileOperationLocker _fileOperationLocker;
    private readonly IFilesDeletingService _filesDeletingService;

    public DeleteFileCommand(
        int fileId,
        string filePath,
        bool askBeforeDelete,
        IFilesLocatingService filesLocatingService,
        IFileOperationLocker fileOperationLocker,
        IFilesDeletingService filesDeletingService)
    {
        _fileId = fileId;
        _filePath = filePath;
        _askBeforeDelete = askBeforeDelete;
        _filesLocatingService = filesLocatingService;
        _fileOperationLocker = fileOperationLocker;
        _filesDeletingService = filesDeletingService;

        _fileOperationLocker.LockStateChanged += OnLockStateChanged;
    }

    public bool CanExecute(object parameter)
    {
        // TODO subscribe to existence event
        return !_fileOperationLocker.IsLocked(_fileId)
               && _filesLocatingService.IsFileExistsAsync(_fileId).Result;
    }

    public void Execute(object parameter)
    {
        try
        {
            using var _ = _fileOperationLocker.Lock(_fileId, TimeSpan.Zero);

            if (_askBeforeDelete
                && MessageBoxHelper.AskForDeletion(_fileId, _filePath) != MessageBoxResult.Yes)
            {
                return;
            }

            _filesDeletingService.DeleteAsync(_fileId).Wait();
        }
        catch (TimeoutException)
        {
        }
    }

    private void OnLockStateChanged(object _, LockStateChangedEventArgs args)
    {
        if (args.FileId == _fileId)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}