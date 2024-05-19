using System.Linq.Expressions;
using Module.Mvvm.Extension.Entities;
using Module.Mvvm.Extension.Services.Abstract;

namespace Module.Mvvm.Extension.Services;

public sealed class ViewModelDependencyService : IViewModelDependencyService
{
    public void RegisterDependency<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
        TDependent dependentViewModel,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyViewModel,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency)
    {
        var dependency = new DependencyHolder<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
            dependentViewModel, dependentProperty, dependencyViewModel, dependencyProperty
        );

        unregisterDependency = () => dependency.Unregister();
        // throw new NotImplementedException();
    }
}