using System.IO;
using System.Windows.Input;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectTorrentFileStepVM : ISelectTorrentFileStepVM
{
    [DependsOn(nameof(TorrentFilePathError))]
    public bool IsValidState => TorrentFilePathError is null;

    [OnChangedMethod(nameof(OnTorrentFilePathChanged))]
    public string TorrentFilePath { get; set; } = string.Empty;

    public string? TorrentFilePathError { get; private set; }

    public ICommand ChangeTorrentFilePath => _changeTorrentFilePath ??= new RelayCommand(ChangeTorrentFilePathCmd);

    private ICommand? _changeTorrentFilePath;

    private readonly ITorrentFileContext _torrentFileContext;
    private readonly IInitialMusicSourceAdditionalInfoContext _additionalInfoContext;

    public SelectTorrentFileStepVM(
        ITorrentFileContext torrentFileContext,
        IInitialMusicSourceAdditionalInfoContext additionalInfoContext)
    {
        _torrentFileContext = torrentFileContext;
        _additionalInfoContext = additionalInfoContext;

        RestoreState();
    }

    public StepResult Confirm()
    {
        var trimmedPath = TorrentFilePath.Trim();
        var fileName = Path.GetFileName(trimmedPath);

        _torrentFileContext.TorrentFilePath = trimmedPath;
        _additionalInfoContext.InitialAdditionalInfo = new MusicSourceAdditionalInfo(
            fileName,
            fileName
        );

        return StepResult.Success;
    }

    private void RestoreState()
    {
        TorrentFilePath = _torrentFileContext.TorrentFilePath ?? string.Empty;
    }

    private void OnTorrentFilePathChanged()
    {
        var trimmedPath = TorrentFilePath.Trim();
        if (trimmedPath.Length == 0)
        {
            TorrentFilePathError = "Path is empty";
        }
        else if (PathHelper.HasInvalidChars(trimmedPath))
        {
            TorrentFilePathError =
                $"Path has invalid chars. Common invalid chars: {string.Join(", ", FilePathHelper.CommonInvalidChars)}";
        }
        else
        {
            TorrentFilePathError = null;
        }
    }

    private void ChangeTorrentFilePathCmd()
    {
        throw new NotImplementedException();
    }
}