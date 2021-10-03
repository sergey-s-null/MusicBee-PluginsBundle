using MusicBeePlugin.Services;
using PropertyChanged;
using Root.MVVM;

namespace MusicBeePlugin.GUI.InboxRelocateContextMenu
{
    [AddINotifyPropertyChangedInterface]
    public class InboxRelocateContextMenuVM
    {
        private RelayCommand? _addToLibraryCommand;
        public RelayCommand AddToLibraryCommand => _addToLibraryCommand ??= new RelayCommand(_ => AddToLibrary());

        private readonly IPluginActions _pluginActions;

        public InboxRelocateContextMenuVM(IPluginActions pluginActions)
        {
            _pluginActions = pluginActions;
        }

        private void AddToLibrary()
        {
            _pluginActions.AddSelectedFileToLibrary();
        }
    }
}