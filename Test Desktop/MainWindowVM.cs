using Root.MVVM;

namespace Test_Desktop
{
    public class MainWindowVM : BaseViewModel
    {
        private readonly ISomeWindowVM _someWindowVM = new SomeWindowVM();
        public ISomeWindowVM SomeWindowVM => _someWindowVM;

    }
}