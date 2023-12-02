using System.Data.Entity;
using Module.Settings.Core;
using Module.Settings.Database.Models;

namespace Module.Settings.Database;

public sealed class SettingsContext : DbContext
{
    public DbSet<SettingEntry> Settings { get; set; } = null!;

    // Used for ef6 util
    // ReSharper disable once UnusedMember.Global
    public SettingsContext()
    {
    }

    public SettingsContext(IModuleConfiguration configuration)
        : base(configuration.DatabaseConnectionString)
    {
    }
}