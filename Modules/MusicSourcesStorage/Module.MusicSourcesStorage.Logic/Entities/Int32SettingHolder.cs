using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.Settings.Database.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class Int32SettingHolder : ISettingHolder<int>
{
    public string Area { get; }
    public string Id { get; }

    public int DefaultValue { get; }

    public int Value
    {
        get => _settingsRepository.FindInt32(Area, Id) ?? DefaultValue;
        set => _settingsRepository.Set(Area, Id, value);
    }

    private readonly ISettingsRepository _settingsRepository;

    public Int32SettingHolder(
        string area,
        string id,
        int defaultValue,
        ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
        Area = area;
        Id = id;
        DefaultValue = defaultValue;
    }
}