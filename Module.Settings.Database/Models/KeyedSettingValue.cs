using System.ComponentModel.DataAnnotations;

namespace Module.Settings.Database.Models;

public sealed class KeyedSettingValue : SettingValue
{
    [Required] public SettingValue Key { get; set; } = null!;
    [Required] public SettingValue Value { get; set; } = null!;
}