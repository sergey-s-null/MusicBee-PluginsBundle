using System.Data.Entity.Migrations;

namespace Module.MusicSourcesStorage.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MusicSourcesStorageContext>
    {
        public const string DesignDatabaseConnectionString = "Data Source=(LocalDb)\\MusicBeePlugins-Design;" +
                                                             "Initial Catalog=MusicSourcesStorage;";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicSourcesStorageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}