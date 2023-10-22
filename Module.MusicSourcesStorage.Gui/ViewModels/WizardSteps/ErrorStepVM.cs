using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class ErrorStepVM : IErrorStepVM
{
    public bool IsValidState => true;

    public string Error { get; }

    private readonly IWizardErrorContext _context;

    public ErrorStepVM(IWizardErrorContext context)
    {
        _context = context;

        Error = context.Error ?? "This is error without error";
    }

    public StepResult Confirm()
    {
        _context.Error = null;
        return StepResult.Success;
    }
}