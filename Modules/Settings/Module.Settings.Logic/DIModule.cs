using Autofac;
using Module.Settings.Logic.Services;
using Module.Settings.Logic.Services.Abstract;

namespace Module.Settings.Logic;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<JsonLoader>()
            .As<IJsonLoader>()
            .SingleInstance();
    }
}