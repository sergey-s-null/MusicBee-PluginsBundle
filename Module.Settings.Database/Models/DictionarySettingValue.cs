namespace Module.Settings.Database.Models;

public sealed class DictionarySettingValue : SettingValue
{
    public List<KeyedSettingValue> Values { get; set; } = new();
}