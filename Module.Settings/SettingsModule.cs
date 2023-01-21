using Autofac;
using Module.MusicBee.Extension;
using Module.Settings.Services;
using Module.Settings.Services.Abstract;

namespace Module.Settings;

public class SettingsModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<MusicBeeExtensionModule>();

        builder
            .RegisterType<SettingsJsonLoader>()
            .As<ISettingsJsonLoader>()
            .SingleInstance();
    }
}