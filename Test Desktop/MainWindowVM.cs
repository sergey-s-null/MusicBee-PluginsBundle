namespace Test_Desktop
{
    public sealed class MainWindowVM
    {
        public ISomeWindowVM SomeWindowVM { get; } = new SomeWindowVM();
    }
}