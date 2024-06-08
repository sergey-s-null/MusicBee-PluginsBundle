using System.Linq.Expressions;
using Module.Mvvm.Extension.Extensions;
using Module.Mvvm.Extension.Services.Abstract;
using PropertyChanged;

namespace Module.Mvvm.Extension.Tests.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class DependentVM
{
    public double Value { get; set; }

    public ChildVM? Child { get; set; } = new();

    private double InternalValue { get; set; }

    public void RegisterIntervalValueDependency<TDependency, TDependencyProperty>(
        IComponentModelDependencyService dependencyService,
        TDependency dependencyObject,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty)
    {
        dependencyService.RegisterDependency(
            this,
            x => x.InternalValue,
            dependencyObject,
            dependencyProperty
        );
    }
}