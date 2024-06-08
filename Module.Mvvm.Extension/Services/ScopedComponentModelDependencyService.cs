using System.Linq.Expressions;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension.Services;

public sealed class ScopedComponentModelDependencyService<T>
    : IScopedComponentModelDependencyService<T>
{
    private readonly T _object;
    private readonly IComponentModelDependencyService _componentModelDependencyService;

    public delegate ScopedComponentModelDependencyService<T> Factory(T @object);

    public ScopedComponentModelDependencyService(
        T @object,
        IComponentModelDependencyService componentModelDependencyService)
    {
        _object = @object;
        _componentModelDependencyService = componentModelDependencyService;
    }

    public void RegisterDependency<TDependentProperty, TDependencyProperty>(
        Expression<Func<T, TDependentProperty>> dependentProperty,
        Expression<Func<T, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency)
    {
        _componentModelDependencyService.RegisterDependency(
            _object,
            dependentProperty,
            _object,
            dependencyProperty,
            out unregisterDependency
        );
    }

    public void RegisterDependency<TDependentProperty, TDependency, TDependencyProperty>(
        Expression<Func<T, TDependentProperty>> dependentProperty,
        TDependency dependencyObject,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency)
    {
        _componentModelDependencyService.RegisterDependency(
            _object,
            dependentProperty,
            dependencyObject,
            dependencyProperty,
            out unregisterDependency
        );
    }
}