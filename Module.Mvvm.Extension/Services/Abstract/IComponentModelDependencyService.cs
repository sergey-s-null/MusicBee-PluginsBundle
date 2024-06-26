﻿using System.ComponentModel;
using System.Linq.Expressions;

namespace Module.Mvvm.Extension.Services.Abstract;

public interface IComponentModelDependencyService
{
    /// <summary>
    /// <b>WARNING!</b><br/>
    /// This logic does not compatible with <c>DependsOnAttribute</c> from PropertyChanged package.<br/>
    /// <br/>
    /// Create dependency based on <see cref="INotifyPropertyChanged"/>.<br/>
    /// <b>Example</b>:<br/>
    /// If property <c>firstObject.Value</c> should depends on <c>secondObject.Settings.Value</c> then you should write
    /// <code>
    /// RegisterDependency(
    ///     firstObject,
    ///     x => x.Value,
    ///     secondObject,
    ///     x => x.Settings.Value,
    ///     ...
    /// )
    /// </code>
    /// In this case <c>firstObject</c> will raise event when either <c>secondObject.Settings</c> or <c>secondObject.Settings.Value</c> changed.
    /// </summary>
    /// <param name="dependentObject">Dependent object implementing <see cref="INotifyPropertyChanged"/>.</param>
    /// <param name="dependentProperty">Direct dependent property.</param>
    /// <param name="dependencyObject">Dependency object implementing <see cref="INotifyPropertyChanged"/>.</param>
    /// <param name="dependencyProperty">Dependency property path.</param>
    /// <param name="unregisterDependency">Callback to unregister dependency.</param>
    void RegisterDependency<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
        TDependent dependentObject,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyObject,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency
    );
}