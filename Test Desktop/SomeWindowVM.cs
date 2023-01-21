using PropertyChanged;

namespace Test_Desktop
{
    [AddINotifyPropertyChangedInterface]
    public sealed class SomeWindowVM : ISomeWindowVM
    {
        public string Field { get; set; } = "l;kasdjfas;lkfjds;   ";
    }
}