using System.Linq.Expressions;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension.Extensions;

public static class ComponentModelDependencyServiceExtensions
{
    public static void RegisterDependency<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
        this IComponentModelDependencyService componentModelDependencyService,
        TDependent dependentObject,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyObject,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty)
    {
        componentModelDependencyService.RegisterDependency(
            dependentObject,
            dependentProperty,
            dependencyObject,
            dependencyProperty,
            out _
        );
    }
}