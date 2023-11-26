using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Settings.Database.Models;

public sealed class SettingEntry
{
    [Key, Column(Order = 0)] public string Area { get; set; } = string.Empty;
    [Key, Column(Order = 1)] public string Id { get; set; } = string.Empty;

    public List<SettingValue> Values { get; set; } = new();
}