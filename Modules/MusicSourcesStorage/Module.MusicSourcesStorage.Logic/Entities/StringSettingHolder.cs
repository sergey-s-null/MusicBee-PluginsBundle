using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.Settings.Database.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class StringSettingHolder : ISettingHolder<string>
{
    public string Area { get; }
    public string Id { get; }

    public string DefaultValue { get; }

    public string Value
    {
        get => _settingsRepository.FindString(Area, Id) ?? DefaultValue;
        set => _settingsRepository.Set(Area, Id, value);
    }

    private readonly ISettingsRepository _settingsRepository;

    public StringSettingHolder(
        string area,
        string id,
        string defaultValue,
        ISettingsRepository settingsRepository)
    {
        Area = area;
        Id = id;
        DefaultValue = defaultValue;

        _settingsRepository = settingsRepository;
    }
}