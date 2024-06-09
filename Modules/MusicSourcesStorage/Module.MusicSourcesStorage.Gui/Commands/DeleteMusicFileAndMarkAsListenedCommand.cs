using System.Windows;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Exceptions;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.Commands;

[AddINotifyPropertyChangedInterface]
public sealed class DeleteMusicFileAndMarkAsListenedCommand : ICommand
{
    private readonly int _fileId;
    private readonly string _filePath;
    private readonly bool _askBeforeDelete;
    private readonly IFileOperationLocker _fileOperationLocker;
    private readonly IFilesDeletingService _filesDeletingService;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;

    public delegate DeleteMusicFileAndMarkAsListenedCommand Factory(
        int fileId,
        string filePath,
        bool askBeforeDelete
    );

    public DeleteMusicFileAndMarkAsListenedCommand(
        int fileId,
        string filePath,
        bool askBeforeDelete,
        IFileOperationLocker fileOperationLocker,
        IFilesDeletingService filesDeletingService,
        IMusicSourcesStorageService musicSourcesStorageService)
    {
        _fileId = fileId;
        _filePath = filePath;
        _askBeforeDelete = askBeforeDelete;
        _fileOperationLocker = fileOperationLocker;
        _filesDeletingService = filesDeletingService;
        _musicSourcesStorageService = musicSourcesStorageService;

        _fileOperationLocker.LockStateChanged += OnLockStateChanged;
    }

    public event EventHandler? CanExecuteChanged;
    public event EventHandler? DeletedAndMarkedAsListened;

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

            if (_askBeforeDelete
                && MessageBoxHelper.AskForDeletion(_fileId, _filePath) != MessageBoxResult.Yes)
            {
                IsProcessing = false;
                return;
            }

            await _filesDeletingService.DeleteAsync(_fileId);
            await _musicSourcesStorageService.SetMusicFileIsListenedAsync(_fileId, isListened: true);

            IsProcessing = false;
            RaiseDeletedAndMarkedAsListened();
        }
        catch (LockTimeoutException)
        {
        }
    }

    private void RaiseDeletedAndMarkedAsListened()
    {
        DeletedAndMarkedAsListened?.Invoke(this, EventArgs.Empty);
    }

    private void OnLockStateChanged(object _, LockStateChangedEventArgs args)
    {
        if (args.FileId == _fileId)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}