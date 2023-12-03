using System.Windows;
using Autofac;
using Module.MusicSourcesStorage.Services.Abstract;

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
            var actions = container.Resolve<IMusicSourcesStorageModuleActions>();
            actions.AddVkPostWithArchiveSource();
        }
    }
}