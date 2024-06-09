using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IFileOperationLocker
{
    event EventHandler<LockStateChangedEventArgs>? LockStateChanged;

    bool IsLocked(int fileId);

    /// <exception cref="LockTimeoutException">Lock was not acquired withing timeout.</exception>
    IDisposable Lock(int fileId, TimeSpan timeout);
}