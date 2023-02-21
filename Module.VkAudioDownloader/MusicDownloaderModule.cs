using Autofac;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.ViewModels;
using Module.VkAudioDownloader.GUI.Views;
using Module.VkAudioDownloader.Services;
using Module.VkAudioDownloader.Services.Abstract;
using Module.VkAudioDownloader.Settings;
using MusicDownloaderSettings = Module.VkAudioDownloader.Settings.MusicDownloaderSettings;

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
                .RegisterType<VkAudiosService>()
                .As<IVkAudiosService>()
                .SingleInstance();
            builder
                .RegisterType<AudioDownloader>()
                .As<IAudioDownloader>()
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

            builder
                .RegisterType<VkAudioDownloaderWindow>()
                .AsSelf();
            builder
                .RegisterType<AuthorizationWindow>()
                .AsSelf();
        }
    }
}