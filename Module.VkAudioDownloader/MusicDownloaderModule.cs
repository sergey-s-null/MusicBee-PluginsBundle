using Autofac;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.ViewModels;
using Module.VkAudioDownloader.Settings;

namespace Module.VkAudioDownloader
{
    public sealed class MusicDownloaderModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MusicDownloaderSettings>()
                .As<IMusicDownloaderSettings>()
                .SingleInstance();

            builder
                .RegisterType<VkAudioDownloaderWindowVM>()
                .As<IVkAudioDownloaderWindowVM>();
            builder
                .RegisterType<MusicDownloaderSettingsVM>()
                .As<IMusicDownloaderSettingsVM>();
            builder
                .RegisterType<AuthorizationWindowVM>()
                .As<IAuthorizationWindowVM>();
        }
    }
}