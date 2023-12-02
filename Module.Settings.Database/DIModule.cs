using Autofac;
using Module.Settings.Database.Services;
using Module.Settings.Database.Services.Abstract;

namespace Module.Settings.Database;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<SettingsRepository>()
            .As<ISettingsRepository>()
            .SingleInstance();
        builder
            .RegisterType<SettingsContext>()
            .AsSelf();
    }
}