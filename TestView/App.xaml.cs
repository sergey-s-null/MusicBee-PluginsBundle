using System;
using System.Windows;
using Module.AudioSourcesComparer.GUI.DesignTimeViewModels;
using Module.AudioSourcesComparer.GUI.Views;
using Module.VkAudioDownloader.GUI.DesignTimeViewModels;
using Module.VkAudioDownloader.GUI.Views;

namespace TestView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = GetWindow(WindowType.VkAudioDownloader);
            window.ShowDialog();
        }

        private static Window GetWindow(WindowType windowType)
        {
            return windowType switch
            {
                WindowType.VkAudioDownloader => new VkAudioDownloaderWindow(new VkAudioDownloaderWindowDTVM()),
                WindowType.VkToLocalComparer => new VkToLocalComparerWindow(new VkToLocalComparerWindowDTVM()),
                _ => throw new ArgumentOutOfRangeException(nameof(windowType), windowType, "Unknown window type.")
            };
        }
    }
}