using System.Linq.Expressions;

namespace Module.Mvvm.Extension.Services.Abstract;

public interface IViewModelDependencyService
{
    void RegisterDependency<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
        TDependent dependentViewModel,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyViewModel,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency
    );
}