﻿using System.Windows.Input;
using Module.Mvvm.Extension;

namespace Module.ArtworksSearcher.GUI.Settings;

public sealed class ArtworksSearcherSettingsDTVM : IArtworksSearcherSettingsVM
{
    public bool Loaded => true;
    public string LoadingErrorMessage => "(ok)";

    public string GoogleCX { get; set; } = "{private google cx}";
    public string GoogleKey { get; set; } = "{private google key}";
    public int ParallelDownloadsCount { get; set; } = 6;
    public string OsuSongsDir { get; set; } = @"C:\Games\osu!\Songs";
    public long MinOsuImageByteSize { get; set; } = 100_000;

    public ICommand ChangeOsuSongsDirCmd { get; } =
        new RelayCommand(_ => throw new NotSupportedException());

    public ICommand ReloadCmd { get; } = RelayCommand.Empty;

    public void Load()
    {
        throw new NotSupportedException();
    }

    public bool Save()
    {
        throw new NotSupportedException();
    }
}