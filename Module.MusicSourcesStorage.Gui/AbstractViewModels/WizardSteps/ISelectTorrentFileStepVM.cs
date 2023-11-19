using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface ISelectTorrentFileStepVM : IManualWizardStepVM
{
    string TorrentFilePath { get; set; }
    string? TorrentFilePathError { get; }

    ICommand ChangeTorrentFilePath { get; }
}