using System.Data.Entity.Migrations;
using Module.MusicSourcesStorage.Database.Factories.Abstract;
using Module.MusicSourcesStorage.Database.Migrations;
using Module.MusicSourcesStorage.Database.Services.Abstract;

namespace Module.MusicSourcesStorage.Database.Services;

public sealed class MusicSourcesStorageMigrator : IMusicSourcesStorageMigrator
{
    private readonly IMusicSourcesStorageContextFactory _contextFactory;

    public MusicSourcesStorageMigrator(IMusicSourcesStorageContextFactory contextFactory)
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