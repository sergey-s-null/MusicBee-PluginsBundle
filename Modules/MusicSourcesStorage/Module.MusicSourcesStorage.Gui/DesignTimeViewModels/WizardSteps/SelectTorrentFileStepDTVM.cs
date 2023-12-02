using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class SelectTorrentFileStepDTVM : ISelectTorrentFileStepVM
{
    public bool IsValidState => TorrentFilePath.Length > 0;

    public string TorrentFilePath { get; set; } = "path/to/file.torrent";

    public string? TorrentFilePathError => TorrentFilePath.Length == 0
        ? "Path is empty"
        : null;

    public ICommand ChangeTorrentFilePath => null!;

    public StepResult Confirm()
    {
        return StepResult.Success;
    }
}