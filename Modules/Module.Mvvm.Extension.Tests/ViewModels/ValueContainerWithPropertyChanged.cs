using PropertyChanged;

namespace Module.Mvvm.Extension.Tests.ViewModels;

[AddINotifyPropertyChangedInterface]
public class ValueContainerWithPropertyChanged : IValueContainer
{
    public int Value { get; set; }
}