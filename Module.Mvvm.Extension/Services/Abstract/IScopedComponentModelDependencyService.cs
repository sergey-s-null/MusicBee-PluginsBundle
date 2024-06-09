using System.Linq.Expressions;

namespace Module.Mvvm.Extension.Services.Abstract;

public interface IScopedComponentModelDependencyService<T>
{
    /// <summary>
    /// Same as <see cref="IComponentModelDependencyService.RegisterDependency{TDependent,TDependentProperty,TDependency,TDependencyProperty}"/>.<br/>
    /// But scoped with <see cref="T"/> object both as dependent and dependency.
    /// </summary>
    void RegisterDependency<TDependentProperty, TDependencyProperty>(
        Expression<Func<T, TDependentProperty>> dependentProperty,
        Expression<Func<T, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency
    );

    /// <summary>
    /// Same as <see cref="IComponentModelDependencyService.RegisterDependency{TDependent,TDependentProperty,TDependency,TDependencyProperty}"/>.<br/>
    /// But scoped with <see cref="T"/> object as dependency.
    /// </summary>
    void RegisterDependency<TDependentProperty, TDependency, TDependencyProperty>(
        Expression<Func<T, TDependentProperty>> dependentProperty,
        TDependency dependencyObject,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency
    );
}