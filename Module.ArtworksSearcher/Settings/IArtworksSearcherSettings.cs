﻿namespace Module.ArtworksSearcher.Settings
{
    public interface IArtworksSearcherSettings
    {
        string GoogleCX { get; set; }
        string GoogleKey { get; set; }
        int MaxParallelDownloadsCount { get; } // todo add to setting and make editable
        int ParallelDownloadsCount { get; set; }
        string OsuSongsDir { get; set; }
        long MinOsuImageByteSize { get; set; }

        bool Load();
        bool Save();
    }
}