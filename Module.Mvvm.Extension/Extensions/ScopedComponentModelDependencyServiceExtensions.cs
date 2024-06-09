using System.Linq.Expressions;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension.Extensions;

public static class ScopedComponentModelDependencyServiceExtensions
{
    public static void RegisterDependency<T, TDependentProperty, TDependencyProperty>(
        this IScopedComponentModelDependencyService<T> dependencyService,
        Expression<Func<T, TDependentProperty>> dependentProperty,
        Expression<Func<T, TDependencyProperty>> dependencyProperty)
    {
        dependencyService.RegisterDependency(dependentProperty, dependencyProperty, out _);
    }

    public static void RegisterDependency<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
        this IScopedComponentModelDependencyService<TDependent> dependencyService,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyObject,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty)
    {
        dependencyService.RegisterDependency(
            dependentProperty,
            dependencyObject,
            dependencyProperty,
            out _
        );
    }
}