using System.Linq.Expressions;
using Module.Mvvm.Extension.Extensions;
using Module.Mvvm.Extension.Services.Abstract;
using PropertyChanged;

namespace Module.Mvvm.Extension.Tests.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class DependencyVM
{
    public int Number { get; set; }

    public ChildVM? Child { get; set; }

    public ValueContainer? Container { get; set; }

    private int InternalNumber { get; set; }

    public void RegisterDependentOnInternalNumberProperty<TDependent, TDependentProperty>(
        IComponentModelDependencyService dependencyService,
        TDependent dependentObject,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty)
    {
        dependencyService.RegisterDependency(
            dependentObject,
            dependentProperty,
            this,
            x => x.InternalNumber
        );
    }

    public void ChangeInternalNumber(int number)
    {
        InternalNumber = number;
    }
}