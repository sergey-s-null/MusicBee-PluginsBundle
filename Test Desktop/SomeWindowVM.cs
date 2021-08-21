using PropertyChanged;
using Root.MVVM;

namespace Test_Desktop
{
    [AddINotifyPropertyChangedInterface]
    public class SomeWindowVM : ISomeWindowVM
    {
        public string Field { get; set; } = "l;kasdjfas;lkfjds;   ";
    }
}