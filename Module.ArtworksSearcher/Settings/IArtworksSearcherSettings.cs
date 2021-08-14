using Root.Abstractions;

namespace Module.ArtworksSearcher.Settings
{
    public interface IArtworksSearcherSettings : ISettings
    {
        string GoogleCX { get; set; }
        string GoogleKey { get; set; }
        int MaxParallelDownloadsCount { get; }
        int ParallelDownloadsCount { get; set; }
        string OsuSongsDir { get; set; }
        long MinOsuImageByteSize { get; set; }
    }
}