using System.Data.Entity;
using Module.Settings.Database.Models;

namespace Module.Settings.Database;

public sealed class SettingsContext : DbContext
{
    public DbSet<SettingEntry> Settings { get; set; } = null!;

    public SettingsContext(string connectionString) : base(connectionString)
    {
    }
}