using Autofac;
using Module.Settings.Core;
using Module.Settings.Database;

namespace Debug.Settings;

public static class Container
{
    public static IContainer Create()
    {
        var builder = new ContainerBuilder();

        // todo dont use "DIModule" directly. Use root SettingsModule after it will be created.
        builder.RegisterModule<DIModule>();

        builder
            .RegisterType<DebugModuleConfiguration>()
            .As<IModuleConfiguration>()
            .SingleInstance();

        return builder.Build();
    }
}