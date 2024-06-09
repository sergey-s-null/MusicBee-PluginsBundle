using Autofac;
using Module.Mvvm.Extension.Services;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension;

public sealed class MvvmExtensionModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterGeneric(typeof(ScopedComponentModelDependencyService<>))
            .AsSelf();
        builder
            .RegisterType<ComponentModelDependencyServiceFactory>()
            .As<IComponentModelDependencyServiceFactory>()
            .SingleInstance();
        builder
            .RegisterType<ComponentModelDependencyService>()
            .As<IComponentModelDependencyService>()
            .SingleInstance();
    }
}