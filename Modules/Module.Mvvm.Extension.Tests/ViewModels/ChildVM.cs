using PropertyChanged;

namespace Module.Mvvm.Extension.Tests.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class ChildVM
{
    public string Text { get; set; } = string.Empty;

    public ChildVM? Child { get; set; }
}