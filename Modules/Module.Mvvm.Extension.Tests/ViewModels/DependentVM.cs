using PropertyChanged;

namespace Module.Mvvm.Extension.Tests.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class DependentVM
{
    public double Value { get; set; }

    public ChildVM? Child { get; set; } = new();
}