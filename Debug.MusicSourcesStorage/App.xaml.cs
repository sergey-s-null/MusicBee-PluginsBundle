using System.Windows;
using Autofac;
using Module.MusicSourcesStorage.Gui.Views;

namespace Debug.MusicSourcesStorage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = Container.Create(true);
            var window = container.Resolve<MusicSourcesWindow>();
            window.ShowDialog();
        }
    }
}