using Autofac;
using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.Gui.Services;
using Module.Vk.Gui.Services.Abstract;
using Module.Vk.GUI.ViewModels;

namespace Module.Vk.Gui;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<VkSettingsVM>()
            .As<IVkSettingsVM>();
        
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