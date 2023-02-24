using Module.Mvvm.Extension;
using Plugin.Main.Services;
using PropertyChanged;

namespace Plugin.Main.GUI.ViewModels;

[AddINotifyPropertyChangedInterface]
public class InboxRelocateContextMenuVM
{
    private RelayCommand? _addToLibraryCommand;

    public RelayCommand AddToLibraryCommand =>
        _addToLibraryCommand ??= new RelayCommand(_ => AddToLibrary());

    private RelayCommand? _retrieveToInboxCommand;

    public RelayCommand RetrieveToInboxCommand =>
        _retrieveToInboxCommand ??= new RelayCommand(_ => RetrieveToInbox());

    private readonly IPluginActions _pluginActions;

    public InboxRelocateContextMenuVM(IPluginActions pluginActions)
    {
        _pluginActions = pluginActions;
    }

    private void AddToLibrary()
    {
        _pluginActions.AddSelectedFileToLibrary();
    }

    private void RetrieveToInbox()
    {
        _pluginActions.RetrieveSelectedFileToInbox();
    }
}