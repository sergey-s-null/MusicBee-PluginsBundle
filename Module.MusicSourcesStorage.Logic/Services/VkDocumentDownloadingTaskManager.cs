using Module.Core.Helpers;
using Module.MusicSourcesStorage.Core.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkDocumentDownloadingTaskManager : IVkDocumentDownloadingTaskManager
{
    private readonly ReaderWriterLockSlim _lock = new();

    private readonly IDictionary<VkOwnedEntityId, FileDownloadingTask> _runningTasks =
        new Dictionary<VkOwnedEntityId, FileDownloadingTask>();

    private readonly IDictionary<VkOwnedEntityId, CompletedFileDownloadingTask> _doneTasks =
        new Dictionary<VkOwnedEntityId, CompletedFileDownloadingTask>();

    private readonly IModuleConfiguration _configuration;
    private readonly IDownloadedVkDocumentsCache _downloadedVkDocumentsCache;

    public VkDocumentDownloadingTaskManager(
        IModuleConfiguration configuration,
        IDownloadedVkDocumentsCache downloadedVkDocumentsCache)
    {
        _configuration = configuration;
        _downloadedVkDocumentsCache = downloadedVkDocumentsCache;
    }

    public IFileDownloadingTask GetOrStartNew(VkDocument document)
    {
        if (TryGetDoneTask(document.Id, out var doneTask))
        {
            return doneTask;
        }

        if (TryGetRunningTask(document.Id, out var runningTask))
        {
            return runningTask;
        }

        if (_downloadedVkDocumentsCache.TryGet(document.Id, out var filePath))
        {
            return AddDoneTask(document.Id, filePath);
        }

        return StartNewDownloadingTask(document);
    }

    public void CancelDownloading(VkDocument document)
    {
        if (!TryPopRunningTask(document.Id, out var task))
        {
            return;
        }

        task.Cancel();
    }

    private string GetFilePath(VkDocument document)
    {
        var fileName = PathHelper.ReplaceInvalidCharacters(document.Name, "_");
        return Path.Combine(_configuration.VkDocumentsDownloadingDirectory, fileName);
    }

    private bool TryGetRunningTask(VkOwnedEntityId documentId, out FileDownloadingTask task)
    {
        _lock.EnterReadLock();
        try
        {
            return _runningTasks.TryGetValue(documentId, out task);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    private bool TryPopRunningTask(VkOwnedEntityId documentId, out FileDownloadingTask task)
    {
        _lock.EnterWriteLock();
        try
        {
            var result = _runningTasks.TryGetValue(documentId, out task);
            if (result)
            {
                _runningTasks.Remove(documentId);
            }

            return result;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private bool TryGetDoneTask(VkOwnedEntityId documentId, out CompletedFileDownloadingTask task)
    {
        _lock.EnterReadLock();
        try
        {
            return _doneTasks.TryGetValue(documentId, out task);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    private void AddRunningTask(VkOwnedEntityId documentId, FileDownloadingTask task)
    {
        _lock.EnterWriteLock();
        try
        {
            _runningTasks[documentId] = task;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private CompletedFileDownloadingTask AddDoneTask(VkOwnedEntityId documentId, string filePath)
    {
        var task = new CompletedFileDownloadingTask(filePath);

        _lock.EnterWriteLock();
        try
        {
            _doneTasks[documentId] = task;
            return task;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private FileDownloadingTask StartNewDownloadingTask(VkDocument document)
    {
        var filePath = GetFilePath(document);
        var newTask = new FileDownloadingTask(document.Uri, filePath, true);
        newTask.Completed += (_, args) => OnTaskCompleted(document.Id, filePath, args.IsSucceeded);
        AddRunningTask(document.Id, newTask);
        newTask.Start();
        return newTask;
    }

    private void OnTaskCompleted(VkOwnedEntityId documentId, string targetFilePath, bool isSucceeded)
    {
        _lock.EnterWriteLock();
        try
        {
            _runningTasks.Remove(documentId);

            if (isSucceeded)
            {
                _doneTasks[documentId] = new CompletedFileDownloadingTask(targetFilePath);
                _downloadedVkDocumentsCache.Add(documentId, targetFilePath);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
}