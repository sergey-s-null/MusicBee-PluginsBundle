using Autofac;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension.Services;

public sealed class ComponentModelDependencyServiceFactory : IComponentModelDependencyServiceFactory
{
    private readonly ILifetimeScope _lifetimeScope;

    public ComponentModelDependencyServiceFactory(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public IScopedComponentModelDependencyService<TDependent> CreateScoped<TDependent>(TDependent dependentObject)
    {
        var factory = _lifetimeScope.Resolve<ScopedComponentModelDependencyService<TDependent>.Factory>();
        return factory(dependentObject);
    }
}