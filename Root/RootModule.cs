using Autofac;
using Root.Services;
using Root.Services.Abstract;

namespace Root
{
    public sealed class RootModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SettingsJsonLoader>()
                .As<ISettingsJsonLoader>()
                .SingleInstance();
        }
    }
}