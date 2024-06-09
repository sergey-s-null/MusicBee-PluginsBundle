using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Exceptions;
using Module.MusicSourcesStorage.Gui.Services;
using Module.MusicSourcesStorage.Gui.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Tests;

public sealed class FileOperationLockerTests
{
    private readonly Random _random = new(143);

    private IFileOperationLocker? _locker;

    [SetUp]
    public void SetUp()
    {
        _locker = new FileOperationLocker();
    }

    [Test]
    public void FileNotLockedByDefault()
    {
        // ARRANGE
        var fileId = GetRandomFileId();

        // ACT
        var isLocked = _locker!.IsLocked(fileId);

        // ASSERT
        Assert.That(isLocked, Is.False);
    }

    [Test]
    public void EventNotRaisedOnIsLockedCheck()
    {
        // ARRANGE
        var lockStateChanged = false;
        _locker!.LockStateChanged += (_, _) => lockStateChanged = true;
        var fileId = GetRandomFileId();

        // ACT
        _locker.IsLocked(fileId);

        // ASSERT
        Assert.That(lockStateChanged, Is.False);
    }

    [Test]
    public void FileLockedAndUnlocked()
    {
        var fileId = GetRandomFileId();

        using (var _ = _locker!.Lock(fileId, TimeSpan.Zero))
        {
            Assert.That(_locker.IsLocked(fileId), Is.True);
        }

        Assert.That(_locker.IsLocked(fileId), Is.False);
    }

    [Test]
    public void LockStateEventRaisedOnLockAndUnlock()
    {
        var fileId = GetRandomFileId();

        var lockStateChangedCount = 0;
        LockStateChangedEventArgs? lockStateChangedArgs = null;
        _locker!.LockStateChanged += (_, args) =>
        {
            lockStateChangedCount++;
            lockStateChangedArgs = args;
        };

        using (var _ = _locker.Lock(fileId, TimeSpan.Zero))
        {
            Assert.Multiple(() =>
            {
                Assert.That(lockStateChangedCount, Is.EqualTo(1));
                Assert.That(lockStateChangedArgs, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(lockStateChangedArgs!.FileId, Is.EqualTo(fileId));
                Assert.That(lockStateChangedArgs.IsLocked, Is.EqualTo(true));
            });
        }

        Assert.Multiple(() =>
        {
            Assert.That(lockStateChangedCount, Is.EqualTo(2));
            Assert.That(lockStateChangedArgs!.FileId, Is.EqualTo(fileId));
            Assert.That(lockStateChangedArgs.IsLocked, Is.EqualTo(false));
        });
    }

    [Test]
    public void TimeoutRaisedOnSecondLock()
    {
        var fileId = GetRandomFileId();

        using var _ = _locker!.Lock(fileId, TimeSpan.Zero);
        Assert.Throws<LockTimeoutException>(() => _locker.Lock(fileId, TimeSpan.Zero));
    }

    [Test]
    public void TwoFilesLockedIndependently()
    {
        var fileIds = GetRandomFileIds(2);

        var unlocker0 = _locker!.Lock(fileIds[0], TimeSpan.Zero);

        Assert.Multiple(() =>
        {
            Assert.That(_locker.IsLocked(fileIds[0]), Is.True);
            Assert.That(_locker.IsLocked(fileIds[1]), Is.False);
        });

        var unlocker1 = _locker.Lock(fileIds[1], TimeSpan.Zero);

        Assert.Multiple(() =>
        {
            Assert.That(_locker.IsLocked(fileIds[0]), Is.True);
            Assert.That(_locker.IsLocked(fileIds[1]), Is.True);
        });

        unlocker0.Dispose();

        Assert.Multiple(() =>
        {
            Assert.That(_locker.IsLocked(fileIds[0]), Is.False);
            Assert.That(_locker.IsLocked(fileIds[1]), Is.True);
        });

        unlocker1.Dispose();

        Assert.Multiple(() =>
        {
            Assert.That(_locker.IsLocked(fileIds[0]), Is.False);
            Assert.That(_locker.IsLocked(fileIds[1]), Is.False);
        });
    }

    private IReadOnlyList<int> GetRandomFileIds(int count)
    {
        var fileIds = new List<int>(count);

        for (var i = 0; i < count; i++)
        {
            var fileId = GetRandomFileId();
            while (fileIds.Contains(fileId))
            {
                fileId++;
            }

            fileIds.Add(fileId);
        }

        return fileIds;
    }

    private int GetRandomFileId()
    {
        return _random.Next(0, 10000);
    }
}