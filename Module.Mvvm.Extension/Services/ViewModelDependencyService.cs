using System.Linq.Expressions;
using Module.Mvvm.Extension.Entities;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension.Services;

public sealed class ViewModelDependencyService : IViewModelDependencyService
{
    public void RegisterDependency<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
        TDependent dependentObject,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyObject,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency)
    {
        var dependency = new DependencyHolder<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
            dependentObject, dependentProperty, dependencyObject, dependencyProperty
        );

        dependency.Register();

        unregisterDependency = () => dependency.Unregister();
    }
}