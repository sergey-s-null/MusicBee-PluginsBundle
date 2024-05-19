using PropertyChanged;

namespace Module.Mvvm.Extension.Tests.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class DerivedVM : BaseVM
{
    public string Text { get; set; } = string.Empty;
}