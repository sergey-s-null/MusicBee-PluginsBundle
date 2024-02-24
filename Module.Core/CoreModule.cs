using Autofac;
using Module.Core.Services;
using Module.Core.Services.Abstract;

namespace Module.Core;

public sealed class CoreModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<SettingsFiles>()
            .As<ISettingsFiles>()
            .SingleInstance();
    }
}