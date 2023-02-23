using System.Windows;
using Module.VkAudioDownloader.GUI.DesignTimeViewModels;
using Module.VkAudioDownloader.GUI.Views;

namespace TestView.VkAudioDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new VkAudioDownloaderWindow(new VkAudioDownloaderWindowDTVM());
            window.Show();
        }
    }
}