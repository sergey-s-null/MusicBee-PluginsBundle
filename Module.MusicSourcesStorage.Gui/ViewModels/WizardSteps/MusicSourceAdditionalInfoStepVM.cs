using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Entities;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourceAdditionalInfoStepVM : IMusicSourceAdditionalInfoStepVM
{
    [OnChangedMethod(nameof(OnNameChanged))]
    public string Name { get; set; } = string.Empty;

    public bool IsValidState { get; private set; }

    private readonly IAddingVkPostWithArchiveContext _context;

    public MusicSourceAdditionalInfoStepVM(IAddingVkPostWithArchiveContext context)
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

        _context.AdditionalInfo = new MusicSourceAdditionalInfo(Name);

        return StepResult.Success;
    }

    private void RestoreState()
    {
        if (_context.AdditionalInfo is not null)
        {
            Name = _context.AdditionalInfo.Name;
        }
        else if (_context.SelectedDocument is not null)
        {
            Name = _context.SelectedDocument.Name;
        }
    }

    private void OnNameChanged()
    {
        IsValidState = Name.Trim().Length != 0;
    }
}