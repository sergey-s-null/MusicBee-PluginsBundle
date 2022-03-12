using Module.PlaylistsExporter.GUI.Settings;
using Module.PlaylistsExporter.Services;
using Module.PlaylistsExporter.Settings;
using Ninject.Modules;
using PlaylistsExporterSettings = Module.PlaylistsExporter.Settings.PlaylistsExporterSettings;

namespace Module.PlaylistsExporter
{
    public class PlaylistsExporterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlaylistsExporterSettings>()
                .To<PlaylistsExporterSettings>()
                .InSingletonScope();
            
            Bind<IPlaylistsExporterSettingsVM>()
                .To<PlaylistsExporterSettingsVM>();

            Bind<IPlaylistToLibraryConverter>()
                .To<PlaylistToLibraryConverter>()
                .InSingletonScope();
            Bind<IPlaylistsExportService>()
                .To<PlaylistsExportService>()
                .InSingletonScope();
        }
    }
}