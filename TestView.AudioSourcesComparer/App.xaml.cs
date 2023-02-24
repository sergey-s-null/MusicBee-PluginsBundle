using System.Windows;
using Autofac;
using Module.AudioSourcesComparer.GUI.Factories;

namespace TestView.AudioSourcesComparer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var container = ApplicationContainer.Create();

        var vkToLocalComparerWindowFactory = container.Resolve<VkToLocalComparerWindowFactory>();

        var window = vkToLocalComparerWindowFactory();

        window.ShowDialog();
    }
}