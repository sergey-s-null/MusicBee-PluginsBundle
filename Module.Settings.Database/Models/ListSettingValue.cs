namespace Module.Settings.Database.Models;

public sealed class ListSettingValue : SettingValue
{
    public List<SettingValue> Values { get; } = new();
}