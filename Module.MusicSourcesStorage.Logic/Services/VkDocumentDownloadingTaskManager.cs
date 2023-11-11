using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkDocumentDownloadingTaskManager : IVkDocumentDownloadingTaskManager
{
    private readonly ReaderWriterLockSlim _lock = new();

    private readonly Dictionary<VkOwnedEntityId, ActivableTaskWithTechnicalInfo<string>> _runningTasks = new();

    private readonly IDownloadedVkDocumentsCache _downloadedVkDocumentsCache;
    private readonly IVkDocumentDownloader _vkDocumentDownloader;

    public VkDocumentDownloadingTaskManager(
        IDownloadedVkDocumentsCache downloadedVkDocumentsCache,
        IVkDocumentDownloader vkDocumentDownloader)
    {
        _downloadedVkDocumentsCache = downloadedVkDocumentsCache;
        _vkDocumentDownloader = vkDocumentDownloader;
    }

    public IActivableTaskWithProgress<VkDocument, string> CreateDownloadTask()
    {
        return new ActivableTaskWithProgressWrapper<VkDocument, string>(GetInternalTask);
    }

    private IActivableWithoutCancellationTaskWithProgress<string> GetInternalTask(
        VkDocument document,
        CancellationToken externalToken)
    {
        if (TryGetDownloadedDocumentPath(document.Id, out var documentPath))
        {
            return new ActivableTaskWithProgress<string>(documentPath)
                .WithToken(externalToken);
        }

        if (!TryGetRunningTask(document.Id, out var taskWithTechnicalInfo))
        {
            taskWithTechnicalInfo = CreateDownloadingTaskWithMetaInfo(document, 0);
            AddRunningTask(document.Id, taskWithTechnicalInfo);
        }

        taskWithTechnicalInfo.IncreaseTaskRequestingCount();

        externalToken.Register(() => OnExternalTaskCancelled(document.Id));

        return taskWithTechnicalInfo.TaskWithEmbeddedToken;
    }

    private bool TryGetDownloadedDocumentPath(VkOwnedEntityId documentId, out string documentPath)
    {
        return _downloadedVkDocumentsCache.TryGet(documentId, out documentPath)
               && File.Exists(documentPath);
    }

    private ActivableTaskWithTechnicalInfo<string> CreateDownloadingTaskWithMetaInfo(
        VkDocument document,
        int taskRequestingInitCount)
    {
        var tokenSource = new CancellationTokenSource();
        var task = _vkDocumentDownloader
            .CreateDownloadTask()
            .WithArgs(document);

        task.SuccessfullyCompleted += (_, args) => OnTaskSuccessfullyCompleted(document.Id, args.Result);
        task.Failed += (_, _) => RemoveRunningTask(document.Id);
        task.Cancelled += (_, _) => RemoveRunningTask(document.Id);

        return new ActivableTaskWithTechnicalInfo<string>(task, tokenSource, taskRequestingInitCount);
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

    private void OnExternalTaskCancelled(VkOwnedEntityId documentId)
    {
        if (!TryGetRunningTask(documentId, out var taskWithMetaInfo))
        {
            return;
        }

        taskWithMetaInfo.DecreaseTaskRequestingCount();

        if (taskWithMetaInfo.TaskRequestingCount == 0)
        {
            CancelInternalTask(taskWithMetaInfo);
            RemoveRunningTask(documentId);
        }
    }

    private void AddRunningTask(
        VkOwnedEntityId documentId,
        ActivableTaskWithTechnicalInfo<string> taskWithTechnicalInfo)
    {
        _lock.EnterWriteLock();
        try
        {
            _runningTasks[documentId] = taskWithTechnicalInfo;
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
        out ActivableTaskWithTechnicalInfo<string> taskWithTechnicalInfo)
    {
        _lock.EnterReadLock();
        try
        {
            return _runningTasks.TryGetValue(documentId, out taskWithTechnicalInfo);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    private static void CancelInternalTask(ActivableTaskWithTechnicalInfo<string> taskWithTechnicalInfo)
    {
        taskWithTechnicalInfo.TokenSource.Cancel();

        try
        {
            taskWithTechnicalInfo.TaskWithProgress.Task.Wait();
        }
        catch (AggregateException e)
        {
            if (e.InnerExceptions.Count != 1 || e.InnerException is not TaskCanceledException)
            {
                throw;
            }
        }
        catch (TaskCanceledException)
        {
        }
    }
}