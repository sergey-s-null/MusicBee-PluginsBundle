﻿using System;
using System.Windows;
using Module.AudioSourcesComparer.GUI.DesignTimeViewModels;
using Module.AudioSourcesComparer.GUI.Views;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Views;
using Module.VkAudioDownloader.GUI.DesignTimeViewModels;
using Module.VkAudioDownloader.GUI.Views;

namespace Debug.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = GetWindow(WindowType.Wizard);
            window.ShowDialog();
        }

        private static Window GetWindow(WindowType windowType)
        {
            return windowType switch
            {
                WindowType.VkAudioDownloader => new VkAudioDownloaderWindow(new VkAudioDownloaderWindowDTVM()),
                WindowType.VkToLocalComparer => new VkToLocalComparerWindow(new VkToLocalComparerWindowDTVM()),
                WindowType.MusicSources => new MusicSourcesWindow(new MusicSourcesWindowDTVM()),
                WindowType.Wizard => new Wizard(_ => new WizardDTVM(new SuccessResultStepDTVM())),
                _ => throw new ArgumentOutOfRangeException(nameof(windowType), windowType, "Unknown window type.")
            };
        }
    }
}