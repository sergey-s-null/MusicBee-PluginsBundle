using PropertyChanged;

namespace Module.Mvvm.Extension.Tests.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class DependencyVM
{
    public int Number { get; set; }

    public ChildVM? Child { get; set; }

    public ValueContainer? Container { get; set; }
}