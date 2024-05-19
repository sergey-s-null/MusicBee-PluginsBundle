using Autofac;
using Module.Mvvm.Extension.Services;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension;

public sealed class MvvmExtensionModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<ComponentModelDependencyService>()
            .As<IComponentModelDependencyService>()
            .SingleInstance();
    }
}