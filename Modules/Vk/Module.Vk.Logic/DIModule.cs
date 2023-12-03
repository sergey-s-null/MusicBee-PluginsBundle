using Autofac;
using Module.Vk.Logic.Services;
using Module.Vk.Logic.Services.Abstract;

namespace Module.Vk.Logic;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<VkSettings>()
            .As<IVkSettings>()
            .SingleInstance();
    }
}