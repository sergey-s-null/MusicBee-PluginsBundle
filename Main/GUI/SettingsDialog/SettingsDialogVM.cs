using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MusicBeePlugin.Annotations;
using Root.Abstractions;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public class SettingsDialogVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ISettings> Settings { get; } = new ();

        public SettingsDialogVM()
        {
            
        }
    }
}