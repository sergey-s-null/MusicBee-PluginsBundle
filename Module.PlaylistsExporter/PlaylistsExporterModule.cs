using Module.PlaylistsExporter.GUI.Settings;
using Module.PlaylistsExporter.Helpers;
using Module.PlaylistsExporter.Services;
using Module.PlaylistsExporter.Settings;
using Ninject.Modules;
using Root.MusicBeeApi.Abstract;

namespace Module.PlaylistsExporter
{
    public class PlaylistsExporterModule : NinjectModule
    {
        private readonly IMusicBeeApi _mbApi;
        
        public PlaylistsExporterModule(IMusicBeeApi mbApi)
        {
            _mbApi = mbApi;
        }
        
        public override void Load()
        {
            Bind<IPlaylistsExporterSettings>()
                .To<PlaylistsExporterSettings>()
                .InSingletonScope()
                .WithConstructorArgument("filePath",
                    ConfigurationHelper.GetSettingsFilePath(_mbApi));
            
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