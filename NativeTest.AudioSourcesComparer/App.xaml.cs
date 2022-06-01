using System.Windows;
using Module.AudioSourcesComparer.GUI.Factories;
using Ninject;

namespace NativeTest.AudioSourcesComparer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var kernel = Bootstrapper.GetKernel();

            var window = kernel
                .Get<IVkToLocalComparerWindowFactory>()
                .Create();

            window.ShowDialog();
        }
    }
}