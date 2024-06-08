using System.Windows.Input;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Exceptions;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.EventArgs;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.Commands;

[AddINotifyPropertyChangedInterface]
public sealed class SelectAsCoverCommand : ICommand
{
    private readonly int _fileId;
    private readonly IFileOperationLocker _fileOperationLocker;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly ICoverSelectionService _coverSelectionService;

    public delegate SelectAsCoverCommand Factory(int fileId);

    public SelectAsCoverCommand(
        int fileId,
        IFileOperationLocker fileOperationLocker,
        IMusicSourcesStorageService musicSourcesStorageService,
        ICoverSelectionService coverSelectionService)
    {
        _fileId = fileId;
        _fileOperationLocker = fileOperationLocker;
        _musicSourcesStorageService = musicSourcesStorageService;
        _coverSelectionService = coverSelectionService;

        _fileOperationLocker.LockStateChanged += OnLockStateChanged;
        _coverSelectionService.CoverChanged += OnCoverChanged;
        _coverSelectionService.CoverRemoved += OnCoverRemoved;
    }

    public event EventHandler? CanExecuteChanged;
    public event EventHandler? Selected;

    public bool IsProcessing { get; private set; }

    public bool CanExecute(object parameter)
    {
        return !_fileOperationLocker.IsLocked(_fileId)
               && !AsyncHelper.Synchronize(() => _musicSourcesStorageService.IsSelectedAsCoverAsync(_fileId));
    }

    public async void Execute(object parameter)
    {
        try
        {
            using var _ = _fileOperationLocker.Lock(_fileId, TimeSpan.Zero);

            IsProcessing = true;

            var task = await _coverSelectionService.CreateCoverSelectionTaskAsync(_fileId);
            await task.Activated(new CoverSelectionArgs(true)).Task;

            IsProcessing = false;
            RaiseSelected();
        }
        catch (LockTimeoutException)
        {
        }
    }

    private void OnLockStateChanged(object _, LockStateChangedEventArgs args)
    {
        if (args.FileId == _fileId)
        {
            RaiseCanExecuteChanged();
        }
    }

    private void OnCoverChanged(object _, CoverChangedEventArgs args)
    {
        if (args.ImageFileId == _fileId)
        {
            RaiseCanExecuteChanged();
        }
    }

    private void OnCoverRemoved(object _, CoverRemovedEventArgs args)
    {
        if (args.FileId == _fileId)
        {
            RaiseCanExecuteChanged();
        }
    }

    private void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private void RaiseSelected()
    {
        Selected?.Invoke(this, EventArgs.Empty);
    }
}