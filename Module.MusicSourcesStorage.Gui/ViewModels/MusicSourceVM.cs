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
    public string Name { get; private set; } = string.Empty;
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

        UpdateFields(additionalInfo);
        Type = type;
        Items = items;
    }

    private void EditCmd()
    {
        var modifiedAdditionalInfo = _wizardService.EditMusicSourceAdditionalInfo(_musicSourceId);
        if (modifiedAdditionalInfo is not null)
        {
            UpdateFields(modifiedAdditionalInfo);
        }
    }

    private void UpdateFields(MusicSourceAdditionalInfo additionalInfo)
    {
        Name = additionalInfo.Name;
    }
}