using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IFileOperationLocker
{
    event EventHandler<LockStateChangedEventArgs>? LockStateChanged;

    bool IsLocked(int fileId);

    /// <exception cref="TimeoutException">Lock was not acquired withing timeout.</exception>
    IDisposable Lock(int fileId, TimeSpan timeout);
}