using Autofac;
using Module.PlaylistsExporter.GUI.Settings;
using Module.PlaylistsExporter.Services;
using Module.PlaylistsExporter.Settings;
using Module.Settings;
using PlaylistsExporterSettings = Module.PlaylistsExporter.Settings.PlaylistsExporterSettings;

namespace Module.PlaylistsExporter
{
    public sealed class PlaylistsExporterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<SettingsModule>();

            builder
                .RegisterType<PlaylistsExporterSettings>()
                .As<IPlaylistsExporterSettings>()
                .SingleInstance();

            builder
                .RegisterType<PlaylistsExporterSettingsVM>()
                .As<IPlaylistsExporterSettingsVM>();

            builder
                .RegisterType<PlaylistToLibraryConverter>()
                .As<IPlaylistToLibraryConverter>()
                .SingleInstance();
            builder
                .RegisterType<PlaylistsExportService>()
                .As<IPlaylistsExportService>()
                .SingleInstance();
        }
    }
}