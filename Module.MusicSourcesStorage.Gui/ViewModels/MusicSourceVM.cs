using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourceVM : IMusicSourceVM
{
    public string Name { get; }
    public MusicSourceType Type { get; }
    public INodesHierarchyVM Items { get; }

    public ICommand Edit => _editCmd ??= new RelayCommand(EditCmd);

    private ICommand? _editCmd;

    public MusicSourceVM(
        string name,
        MusicSourceType type,
        INodesHierarchyVM items)
    {
        Name = name;
        Type = type;
        Items = items;
    }

    private void EditCmd()
    {
        throw new NotImplementedException();
    }
}