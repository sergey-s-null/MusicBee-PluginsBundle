using System.Data.Entity;
using Module.MusicSourcesStorage.Core;
using Module.MusicSourcesStorage.Database.Models;

namespace Module.MusicSourcesStorage.Database;

public sealed class MusicSourcesStorageContext : DbContext
{
    public DbSet<MusicSourceModel> Sources { get; set; } = null!;
    public DbSet<FileModel> Files { get; set; } = null!;

    // Used for ef6 migrations
    // ReSharper disable once UnusedMember.Global
    public MusicSourcesStorageContext()
    {
    }

    public MusicSourcesStorageContext(IModuleConfiguration configuration)
        : base(configuration.DatabaseConnectionString)
    {
    }
}