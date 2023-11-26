using System.Data.Entity;
using Module.Settings.Database.Models;
using Module.Settings.Database.Services.Abstract;

namespace Module.Settings.Database.Services;

public sealed class SettingsRepository : ISettingsRepository
{
    private readonly Func<SettingsContext> _contextFactory;

    public SettingsRepository(Func<SettingsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public string? FindString(string area, string id)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Values)
            .FirstOrDefault(x => x.Area == area && x.Id == id);
        if (setting is null || setting.Values.Count == 0)
        {
            return null;
        }

        var value = setting.Values.First();
        return value is StringSettingValue stringValue
            ? stringValue.Value
            : null;
    }

    public int? FindInt32(string area, string id)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Values)
            .FirstOrDefault(x => x.Area == area && x.Id == id);
        if (setting is null || setting.Values.Count == 0)
        {
            return null;
        }

        var value = setting.Values.First();
        return value is Int32SettingValue int32Value
            ? int32Value.Value
            : null;
    }

    public void Set(string area, string id, string value)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Values)
            .FirstOrDefault(x => x.Area == area && x.Id == id);

        if (setting is not null)
        {
            setting.Values = new List<SettingValue>
            {
                new StringSettingValue { Value = value }
            };
        }
        else
        {
            context.Settings.Add(new SettingEntry
            {
                Area = area,
                Id = id,
                Values =
                {
                    new StringSettingValue { Value = value }
                }
            });
        }

        context.SaveChanges();
    }

    public void Set(string area, string id, int value)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Values)
            .FirstOrDefault(x => x.Area == area && x.Id == id);

        if (setting is not null)
        {
            setting.Values = new List<SettingValue>
            {
                new Int32SettingValue { Value = value }
            };
        }
        else
        {
            context.Settings.Add(new SettingEntry
            {
                Area = area,
                Id = id,
                Values =
                {
                    new Int32SettingValue { Value = value }
                }
            });
        }

        context.SaveChanges();
    }
}