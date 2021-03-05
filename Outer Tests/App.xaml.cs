using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VkMusicDownloader.GUI;
using VkNet;

namespace Outer_Tests
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IMainWindowVM vm = new TestMainWindowVM();
            
            var wnd = new VkMusicDownloader.GUI.MainWindow(vm);

            wnd.ShowDialog();
        }
    }
}
