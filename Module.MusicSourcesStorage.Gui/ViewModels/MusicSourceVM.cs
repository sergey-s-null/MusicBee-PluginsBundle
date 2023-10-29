using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
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

    private readonly int _musicSourceId;
    private readonly IWizardService _wizardService;

    public MusicSourceVM(
        int musicSourceId,
        MusicSourceAdditionalInfo additionalInfo,
        MusicSourceType type,
        INodesHierarchyVM items,
        IWizardService wizardService)
    {
        _musicSourceId = musicSourceId;
        _wizardService = wizardService;

        Name = additionalInfo.Name;
        Type = type;
        Items = items;
    }

    private void EditCmd()
    {
        _wizardService.EditMusicSourceAdditionalInfo(_musicSourceId);
        // todo update current state
        throw new NotImplementedException();
    }
}