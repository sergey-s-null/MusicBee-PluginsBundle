using System.Windows;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourceVM : IMusicSourceVM
{
    public event EventHandler? Deleted;

    public string Name { get; private set; } = string.Empty;
    public MusicSourceType Type { get; }
    public INodesHierarchyVM Items { get; }

    public ICommand Edit => _editCmd ??= new RelayCommand(EditCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);

    private ICommand? _editCmd;
    private ICommand? _deleteCmd;

    private readonly int _musicSourceId;
    private readonly IWizardService _wizardService;
    private readonly IMusicSourcesStorageService _storageService;

    public MusicSourceVM(
        int musicSourceId,
        MusicSourceAdditionalInfo additionalInfo,
        MusicSourceType type,
        INodesHierarchyVM items,
        IWizardService wizardService,
        IMusicSourcesStorageService storageService)
    {
        _musicSourceId = musicSourceId;
        _wizardService = wizardService;
        _storageService = storageService;

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

    private async void DeleteCmd()
    {
        var result = MessageBox.Show(
            $"Are you sure you want to delete source \"{Name}\" with id {_musicSourceId}?",
            "\u255a(•\u2302•)\u255d",
            MessageBoxButton.YesNo,
            MessageBoxImage.Stop
        );

        if (result == MessageBoxResult.Yes)
        {
            await _storageService.DeleteMusicSourceAsync(_musicSourceId);
            Deleted?.Invoke(this, EventArgs.Empty);
        }
    }

    private void UpdateFields(MusicSourceAdditionalInfo additionalInfo)
    {
        Name = additionalInfo.Name;
    }
}