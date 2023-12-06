using System.Data.Entity;
using Module.MusicSourcesStorage.Core;
using Module.MusicSourcesStorage.Database.Factories.Abstract;
using Module.MusicSourcesStorage.Database.Models;

namespace Module.MusicSourcesStorage.Database;

/// <remarks>
/// Use <see cref="IMusicSourcesStorageContextFactory"/>.<see cref="IMusicSourcesStorageContextFactory.Create"/>
/// for constructing to avoid constructing with empty constructor.
/// </remarks>
public sealed class MusicSourcesStorageContext : DbContext
{
    public DbSet<MusicSourceModel> Sources { get; set; } = null!;
    public DbSet<FileModel> Files { get; set; } = null!;

    // Used for ef6 migrations
    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// Constructor for design purposes only: migrations etc.
    /// </summary>
    public MusicSourcesStorageContext()
        : base(Migrations.Configuration.DesignDatabaseConnectionString)
    {
    }

    public MusicSourcesStorageContext(IModuleConfiguration configuration)
        : base(configuration.DatabaseConnectionString)
    {
    }
}