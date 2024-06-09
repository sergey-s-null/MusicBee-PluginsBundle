using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Exceptions;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.Commands;

[AddINotifyPropertyChangedInterface]
public sealed class ChangeMusicListenedStateCommand : ICommand
{
    private readonly int _fileId;
    private readonly bool _isListened;
    private readonly IFileOperationLocker _fileOperationLocker;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;

    public delegate ChangeMusicListenedStateCommand Factory(
        int fileId,
        bool isListened
    );

    public ChangeMusicListenedStateCommand(
        int fileId,
        bool isListened,
        IFileOperationLocker fileOperationLocker,
        IMusicSourcesStorageService musicSourcesStorageService)
    {
        _fileId = fileId;
        _isListened = isListened;
        _fileOperationLocker = fileOperationLocker;
        _musicSourcesStorageService = musicSourcesStorageService;

        _fileOperationLocker.LockStateChanged += OnLockStateChanged;
    }

    public event EventHandler? CanExecuteChanged;
    public event EventHandler? Changed;

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

            await _musicSourcesStorageService.SetMusicFileIsListenedAsync(_fileId, _isListened);

            IsProcessing = false;
            RaiseChanged();
        }
        catch (LockTimeoutException)
        {
        }
    }

    private void RaiseChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }

    private void OnLockStateChanged(object _, LockStateChangedEventArgs args)
    {
        if (args.FileId == _fileId)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}