using Root.MVVM;

namespace Test_Desktop
{
    public class SomeWindowVM : BaseViewModel, ISomeWindowVM
    {
        private string _field = "l;kasdjfas;lkfjds;   ";
        public string Field
        {
            get => _field;
            set
            {
                _field = value;
                NotifyPropChanged(nameof(Field));
            }
        }
    }
}