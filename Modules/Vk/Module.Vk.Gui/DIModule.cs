using Autofac;
using Module.Vk.Gui.AbstractViewModels;
using Module.Vk.Gui.Services;
using Module.Vk.Gui.Services.Abstract;
using Module.Vk.Gui.ViewModels;
using Module.Vk.Gui.Views;

namespace Module.Vk.Gui;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AuthorizationWindow>()
            .AsSelf();

        builder
            .RegisterType<VkSettingsVM>()
            .As<IVkSettingsVM>();
        builder
            .RegisterType<AuthorizationWindowVM>()
            .As<IAuthorizationWindowVM>();

        builder
            .RegisterType<VkApiProvider>()
            .As<IVkApiProvider>()
            .SingleInstance();
        builder
            .RegisterType<VkApiAuthorizationsService>()
            .As<IVkApiAuthorizationsService>()
            .SingleInstance();
    }
}