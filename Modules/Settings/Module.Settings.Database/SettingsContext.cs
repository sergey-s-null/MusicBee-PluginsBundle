using System.Data.Entity;
using Module.Settings.Core;
using Module.Settings.Database.Models;
using Module.Settings.Database.Services.Abstract;

namespace Module.Settings.Database;

/// <remarks>
/// Use <see cref="ISettingsContextFactory"/>.<see cref="ISettingsContextFactory.Create"/>
/// for constructing to avoid constructing with empty constructor.
/// </remarks>
public sealed class SettingsContext : DbContext
{
    public DbSet<SettingEntry> Settings { get; set; } = null!;

    // Used for ef6 util
    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// Constructor for design purposes only: migrations etc.
    /// </summary>
    public SettingsContext()
        : base(Migrations.Configuration.DesignDatabaseConnectionString)
    {
    }

    public SettingsContext(IModuleConfiguration configuration)
        : base(configuration.DatabaseConnectionString)
    {
    }
}