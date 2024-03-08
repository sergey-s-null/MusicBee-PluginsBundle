using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.EventArgs;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Commands;

public sealed class SelectAsCoverCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly int _fileId;
    private readonly int _sourceId;
    private readonly IFileOperationLocker _fileOperationLocker;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly ICoverSelectionService _coverSelectionService;

    public SelectAsCoverCommand(
        int fileId,
        int sourceId,
        IFileOperationLocker fileOperationLocker,
        IMusicSourcesStorageService musicSourcesStorageService,
        ICoverSelectionService coverSelectionService)
    {
        _fileId = fileId;
        _sourceId = sourceId;
        _fileOperationLocker = fileOperationLocker;
        _musicSourcesStorageService = musicSourcesStorageService;
        _coverSelectionService = coverSelectionService;

        _fileOperationLocker.LockStateChanged += OnLockStateChanged;
        _coverSelectionService.CoverChanged += OnCoverChanged;
        _coverSelectionService.CoverRemoved += OnCoverRemoved;
    }

    public bool CanExecute(object parameter)
    {
        return !_fileOperationLocker.IsLocked(_fileId)
               && !_musicSourcesStorageService.IsSelectedAsCoverAsync(_fileId).Result;
    }

    public void Execute(object parameter)
    {
        try
        {
            using var _ = _fileOperationLocker.Lock(_fileId, TimeSpan.Zero);

            var task = _coverSelectionService.CreateCoverSelectionTaskAsync(_fileId).Result;
            task.Activated(new CoverSelectionArgs(true)).Task.Wait();
        }
        catch (TimeoutException)
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
        if (args.SourceId == _sourceId)
        {
            RaiseCanExecuteChanged();
        }
    }

    private void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}