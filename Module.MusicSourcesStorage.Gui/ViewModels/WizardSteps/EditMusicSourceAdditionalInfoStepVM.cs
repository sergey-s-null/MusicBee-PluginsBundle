using System.IO;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
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

    private readonly IEditMusicSourceAdditionalInfoContext _context;

    public EditMusicSourceAdditionalInfoStepVM(IEditMusicSourceAdditionalInfoContext context)
    {
        _context = context;

        ValidateContext();
        RestoreState();
    }

    public StepResult Confirm()
    {
        if (!IsValidState)
        {
            throw new InvalidOperationException();
        }

        _context.EditedAdditionalInfo = new MusicSourceAdditionalInfo(Name, TargetFilesDirectory.Trim());

        return StepResult.Success;
    }

    private void RestoreState()
    {
        Name = _context.InitialAdditionalInfo!.Name;
        TargetFilesDirectory = _context.InitialAdditionalInfo.TargetFilesDirectory;
    }

    private void OnNameChanged()
    {
        NameError = Name.Trim().Length == 0
            ? "Name is empty"
            : null;
    }

    private void OnTargetFilesDirectoryChanged()
    {
        var trimmedDirectory = TargetFilesDirectory.Trim();

        if (trimmedDirectory.Length == 0)
        {
            TargetFilesDirectoryError = "Path is empty";
        }
        else if (PathHelper.HasInvalidChars(trimmedDirectory))
        {
            TargetFilesDirectoryError =
                "Path has invalid chars. Common invalid chars: \", <, >, |, :, *, ?, \\, /";
        }
        else if (Path.IsPathRooted(trimmedDirectory))
        {
            TargetFilesDirectoryError = "Path is rooted";
        }
        else
        {
            TargetFilesDirectoryError = null;
        }
    }

    private void ValidateContext()
    {
        _context.ValidateHasInitialAdditionalInfo();
    }
}