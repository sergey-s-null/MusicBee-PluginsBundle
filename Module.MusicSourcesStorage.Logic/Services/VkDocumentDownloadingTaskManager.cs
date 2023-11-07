using Module.Core.Helpers;
using Module.MusicSourcesStorage.Core.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkDocumentDownloadingTaskManager : IVkDocumentDownloadingTaskManager
{
    private readonly ReaderWriterLockSlim _lock = new();

    private readonly Dictionary<VkOwnedEntityId, FileDownloadingTaskWithMetaInfo> _runningTasks = new();
    private readonly Dictionary<VkOwnedEntityId, CompletedFileDownloadingTask> _doneTasks = new();

    private readonly IModuleConfiguration _configuration;
    private readonly IDownloadedVkDocumentsCache _downloadedVkDocumentsCache;

    public VkDocumentDownloadingTaskManager(
        IModuleConfiguration configuration,
        IDownloadedVkDocumentsCache downloadedVkDocumentsCache)
    {
        _configuration = configuration;
        _downloadedVkDocumentsCache = downloadedVkDocumentsCache;
    }

    public ITaskWithProgress<string> GetOrCreateNewAsync(
        VkDocument document,
        bool activateTask,
        CancellationToken token)
    {
        if (TryGetDoneTask(document.Id, out var doneTask))
        {
            return doneTask;
        }

        if (_downloadedVkDocumentsCache.TryGet(document.Id, out var filePath))
        {
            return AddDoneTask(document.Id, filePath);
        }

        if (TryGetRunningTask(document.Id, out var task))
        {
            task.IncreaseTaskRequestingCount();
        }
        else
        {
            task = CreateDownloadingTaskWithMetaInfo(document, 1);
            AddRunningTask(document.Id, task);
        }

        token.Register(() => OnExternalTaskCancelled(document.Id));

        var wrappedTask = new TaskCancellationWrapper<string>(task.Task, token);
        if (activateTask)
        {
            wrappedTask.Activate();
        }

        return wrappedTask;
    }

    private FileDownloadingTaskWithMetaInfo CreateDownloadingTaskWithMetaInfo(
        VkDocument document,
        int taskRequestingInitCount)
    {
        var filePath = GetFilePath(document);

        var task = new FileDownloadingTask(document.Uri, filePath, true);

        task.SuccessfullyCompleted += (_, args) => OnTaskSuccessfullyCompleted(document.Id, args.Result);
        task.Failed += (_, _) => RemoveRunningTask(document.Id);
        task.Cancelled += (_, _) => RemoveRunningTask(document.Id);

        return new FileDownloadingTaskWithMetaInfo(task, new CancellationTokenSource(), taskRequestingInitCount);
    }

    private string GetFilePath(VkDocument document)
    {
        var fileName = PathHelper.ReplaceInvalidCharacters(document.Name, "_");
        return Path.Combine(_configuration.VkDocumentsDownloadingDirectory, fileName);
    }

    private void OnTaskSuccessfullyCompleted(VkOwnedEntityId documentId, string targetFilePath)
    {
        _lock.EnterWriteLock();
        try
        {
            _runningTasks.Remove(documentId);
            _doneTasks[documentId] = new CompletedFileDownloadingTask(targetFilePath);
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
        FileDownloadingTaskWithMetaInfo taskWithMetaInfo)
    {
        _lock.EnterWriteLock();
        try
        {
            _runningTasks[documentId] = taskWithMetaInfo;
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
        out FileDownloadingTaskWithMetaInfo taskWithMetaInfo)
    {
        _lock.EnterReadLock();
        try
        {
            return _runningTasks.TryGetValue(documentId, out taskWithMetaInfo);
        }
        finally
        {
            _lock.ExitReadLock();
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

    private static void CancelInternalTask(FileDownloadingTaskWithMetaInfo taskWithMetaInfo)
    {
        taskWithMetaInfo.TokenSource.Cancel();

        try
        {
            taskWithMetaInfo.Task.Task.Wait();
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