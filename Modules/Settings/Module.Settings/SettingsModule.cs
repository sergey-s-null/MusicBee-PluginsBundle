using Autofac;
using LogicDIModule = Module.Settings.Logic.DIModule;
using DatabaseDIModule = Module.Settings.Database.DIModule;

namespace Module.Settings;

public sealed class SettingsModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<LogicDIModule>();
        builder.RegisterModule<DatabaseDIModule>();
    }
}