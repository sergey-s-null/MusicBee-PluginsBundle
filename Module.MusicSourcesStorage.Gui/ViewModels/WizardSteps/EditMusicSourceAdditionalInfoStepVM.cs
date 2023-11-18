using System.IO;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Entities;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class EditMusicSourceAdditionalInfoStepVM : IEditMusicSourceAdditionalInfoStepVM
{
    [OnChangedMethod(nameof(OnNameChanged))]
    public string Name { get; set; } = string.Empty;

    public string? NameError { get; private set; }

    [OnChangedMethod(nameof(OnTargetFilesDirectoryChanged))]
    public string TargetFilesDirectory { get; set; } = string.Empty;

    public string? TargetFilesDirectoryError { get; private set; }

    [DependsOn(nameof(NameError), nameof(TargetFilesDirectoryError))]
    public bool IsValidState => NameError is null && TargetFilesDirectoryError is null;

    private readonly IMusicSourceAdditionalInfoContext _context;

    public EditMusicSourceAdditionalInfoStepVM(IMusicSourceAdditionalInfoContext context)
    {
        _context = context;

        RestoreState();
    }

    public StepResult Confirm()
    {
        if (!IsValidState)
        {
            throw new InvalidOperationException();
        }

        _context.AdditionalInfo = new MusicSourceAdditionalInfo(Name, TargetFilesDirectory);

        return StepResult.Success;
    }

    private void RestoreState()
    {
        Name = _context.AdditionalInfo?.Name ?? string.Empty;
        TargetFilesDirectory = _context.AdditionalInfo?.TargetFilesDirectory ?? string.Empty;
    }

    private void OnNameChanged()
    {
        NameError = Name.Trim().Length == 0
            ? "Name is empty"
            : null;
    }

    private void OnTargetFilesDirectoryChanged()
    {
        if (TargetFilesDirectory.Trim().Length == 0)
        {
            TargetFilesDirectoryError = "Path is empty";
        }
        else if (PathHelper.HasInvalidChars(TargetFilesDirectory))
        {
            TargetFilesDirectoryError =
                $"Path has invalid chars. Invalid chars: {string.Join(", ", Path.GetInvalidPathChars())}";
        }
        else if (Path.IsPathRooted(TargetFilesDirectory))
        {
            TargetFilesDirectoryError = "Path is rooted";
        }
        else
        {
            TargetFilesDirectoryError = null;
        }
    }
}