using System.Data.Entity.Migrations;
using Module.Settings.Database.Migrations;
using Module.Settings.Database.Services.Abstract;

namespace Module.Settings.Database.Services;

public sealed class SettingsDbMigrator : ISettingsDbMigrator
{
    private readonly ISettingsContextFactory _contextFactory;

    public SettingsDbMigrator(ISettingsContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public void UpdateToLatest()
    {
        using var context = _contextFactory.Create();
        var configuration = new Configuration();
        var migrator = new DbMigrator(configuration, context);
        migrator.Update();
    }
}