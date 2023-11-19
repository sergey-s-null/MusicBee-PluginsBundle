using System.Windows.Input;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectTorrentFileStepVM : ISelectTorrentFileStepVM
{
    [DependsOn(nameof(TorrentFilePathError))]
    public bool IsValidState => TorrentFilePathError is null;

    [OnChangedMethod(nameof(OnTorrentFilePathChanged))]
    public string TorrentFilePath { get; set; }

    public string? TorrentFilePathError { get; private set; }

    public ICommand ChangeTorrentFilePath => _changeTorrentFilePath ??= new RelayCommand(ChangeTorrentFilePathCmd);

    private ICommand? _changeTorrentFilePath;

    private readonly ITorrentFileContext _torrentFileContext;

    public SelectTorrentFileStepVM(ITorrentFileContext torrentFileContext)
    {
        _torrentFileContext = torrentFileContext;

        TorrentFilePath = string.Empty;
    }

    public StepResult Confirm()
    {
        _torrentFileContext.TorrentFilePath = TorrentFilePath.Trim();

        return StepResult.Success;
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
                $"Path has invalid chars. Common invalid chars: {string.Join(", ", PathHelper.CommonInvalidChars)}";
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