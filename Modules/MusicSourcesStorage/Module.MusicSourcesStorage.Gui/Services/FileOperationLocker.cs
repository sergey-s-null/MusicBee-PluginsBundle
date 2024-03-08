using System.Collections.Concurrent;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class FileOperationLocker : IFileOperationLocker
{
    public event EventHandler<LockStateChangedEventArgs>? LockStateChanged;

    private readonly ConcurrentDictionary<int, SemaphoreSlim> _locks = new();

    public bool IsLocked(int fileId)
    {
        if (!_locks.TryGetValue(fileId, out var lockObject))
        {
            return false;
        }

        return lockObject.CurrentCount == 0;
    }

    public IDisposable Lock(int fileId, TimeSpan timeout)
    {
        var lockObject = GetLockObject(fileId);

        if (!lockObject.Wait(timeout))
        {
            throw new TimeoutException($"Could not acquire lock for file {fileId} after timeout {timeout}.");
        }

        RaiseLocked(fileId);

        return new Unlocker(lockObject, () => RaiseUnlocked(fileId));
    }

    private SemaphoreSlim GetLockObject(int fileId)
    {
        return _locks.GetOrAdd(fileId, _ => new SemaphoreSlim(1));
    }

    private void RaiseLocked(int fileId)
    {
        RaiseLockStateChangedEvent(fileId, true);
    }

    private void RaiseUnlocked(int fileId)
    {
        RaiseLockStateChangedEvent(fileId, false);
    }

    private void RaiseLockStateChangedEvent(int fileId, bool isLocked)
    {
        LockStateChanged?.Invoke(
            this,
            new LockStateChangedEventArgs(fileId, isLocked)
        );
    }

    private sealed class Unlocker : IDisposable
    {
        private readonly SemaphoreSlim _lockObject;
        private readonly Action _unlockedCallback;

        public Unlocker(SemaphoreSlim lockObject, Action unlockedCallback)
        {
            _lockObject = lockObject;
            _unlockedCallback = unlockedCallback;
        }

        public void Dispose()
        {
            _lockObject.Release();
            _unlockedCallback();
        }
    }
}