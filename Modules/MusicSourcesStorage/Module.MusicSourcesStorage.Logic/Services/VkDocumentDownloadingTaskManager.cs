using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkDocumentDownloadingTaskManager : IVkDocumentDownloadingTaskManager
{
    private readonly ReaderWriterLockSlim _lock = new();

    private readonly Dictionary<VkOwnedEntityId, ISharedTask<string>> _runningTasks = new();

    private readonly IDownloadedVkDocumentsCache _downloadedVkDocumentsCache;
    private readonly IVkDocumentDownloader _vkDocumentDownloader;

    public VkDocumentDownloadingTaskManager(
        IDownloadedVkDocumentsCache downloadedVkDocumentsCache,
        IVkDocumentDownloader vkDocumentDownloader)
    {
        _downloadedVkDocumentsCache = downloadedVkDocumentsCache;
        _vkDocumentDownloader = vkDocumentDownloader;
    }

    public IActivableTask<VkDocument, string> CreateDownloadTask()
    {
        return new SharedTaskWrapper<VkDocument, string>(GetInternalTask);
    }

    private ISharedTask<string> GetInternalTask(VkDocument document)
    {
        if (TryGetDownloadedDocumentPath(document.Id, out var documentPath))
        {
            return ActivableTaskFactory
                .FromResult(documentPath)
                .ToShared();
        }

        return GetOrCreateDownloadingTask(document);
    }

    private bool TryGetDownloadedDocumentPath(VkOwnedEntityId documentId, out string documentPath)
    {
        return _downloadedVkDocumentsCache.TryGet(documentId, out documentPath)
               && File.Exists(documentPath);
    }

    private ISharedTask<string> GetOrCreateDownloadingTask(VkDocument document)
    {
        if (TryGetRunningTask(document.Id, out var task))
        {
            return task;
        }

        task = CreateDownloadingTask(document);
        AddRunningTask(document.Id, task);

        return task;
    }

    private ISharedTask<string> CreateDownloadingTask(VkDocument document)
    {
        var task = new SharedTask<string>(() =>
            _vkDocumentDownloader
                .CreateDownloadTask()
                .WithArgs(document)
        );

        task.SuccessfullyCompleted += (_, args) => OnTaskSuccessfullyCompleted(document.Id, args.Result);
        task.Failed += (_, _) => RemoveRunningTask(document.Id);
        task.Cancelled += (_, _) => RemoveRunningTask(document.Id);

        return task;
    }

    private void OnTaskSuccessfullyCompleted(VkOwnedEntityId documentId, string targetFilePath)
    {
        _lock.EnterWriteLock();
        try
        {
            _runningTasks.Remove(documentId);
            _downloadedVkDocumentsCache.Add(documentId, targetFilePath);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private void AddRunningTask(
        VkOwnedEntityId documentId,
        ISharedTask<string> task)
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

    private void RemoveRunningTask(VkOwnedEntityId documentId)
    {
        _lock.EnterWriteLock();
        try
        {
            _runningTasks.Remove(documentId);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private bool TryGetRunningTask(
        VkOwnedEntityId documentId,
        out ISharedTask<string> task)
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
}