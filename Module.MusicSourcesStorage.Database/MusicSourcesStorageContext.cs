using System.Data.Entity;
using Module.MusicSourcesStorage.Database.Models;
using File = Module.MusicSourcesStorage.Database.Models.File;

namespace Module.MusicSourcesStorage.Database;

public sealed class MusicSourcesStorageContext : DbContext
{
    public DbSet<MusicSource> Sources { get; set; } = null!;
    public DbSet<File> Files { get; set; } = null!;

    public MusicSourcesStorageContext(string connectionString) : base(connectionString)
    {
    }
}