using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Newtonsoft.Json;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class DownloadedVkDocumentsCache : IDownloadedVkDocumentsCache
{
    private const string MetaInfoFileName = "downloaded-archives.json";

    private readonly ReaderWriterLockSlim _lock = new();

    private readonly Lazy<string> _metaInfoFilePath;
    private readonly Lazy<IDictionary<VkOwnedEntityId, string>> _downloadedVkDocuments;

    public DownloadedVkDocumentsCache(IMusicSourcesStorageSettingsAccessor settingsAccessor)
    {
        _metaInfoFilePath = new Lazy<string>(() =>
            Path.Combine(settingsAccessor.VkDocumentsDownloadingDirectory, MetaInfoFileName)
        );
        _downloadedVkDocuments = new Lazy<IDictionary<VkOwnedEntityId, string>>(LoadMetaInfo);
    }

    public void Add(VkOwnedEntityId documentId, string filePath)
    {
        _lock.EnterWriteLock();
        try
        {
            _downloadedVkDocuments.Value[documentId] = filePath;
            SaveMetaInfo();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool TryGet(VkOwnedEntityId documentId, out string filePath)
    {
        _lock.EnterReadLock();
        try
        {
            return _downloadedVkDocuments.Value.TryGetValue(documentId, out filePath);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    private IDictionary<VkOwnedEntityId, string> LoadMetaInfo()
    {
        if (!File.Exists(_metaInfoFilePath.Value))
        {
            return new Dictionary<VkOwnedEntityId, string>();
        }

        var json = File.ReadAllText(_metaInfoFilePath.Value);
        var entries = JsonConvert.DeserializeObject<List<(VkOwnedEntityId, string)>>(json);
        return entries is null
            ? new Dictionary<VkOwnedEntityId, string>()
            : entries.ToDictionary(x => x.Item1, x => x.Item2);
    }

    private void SaveMetaInfo()
    {
        var entries = _downloadedVkDocuments.Value
            .Select(x => (x.Key, x.Value))
            .ToList();
        var json = JsonConvert.SerializeObject(entries);
        File.WriteAllText(_metaInfoFilePath.Value, json);
    }
}