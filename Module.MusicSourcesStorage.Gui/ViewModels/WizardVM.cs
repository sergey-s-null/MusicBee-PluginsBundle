using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class WizardVM : IWizardVM
{
    public IWizardStepVM CurrentStep { get; private set; }

    private readonly Window _ownerWindow;

    public WizardVM(Window ownerWindow, IWizardStepVM initialStep)
    {
        _ownerWindow = ownerWindow;

        AddEventHandlers(initialStep);
        StartIfAutomatic(initialStep);
        CurrentStep = initialStep;
    }

    private void AddEventHandlers(IWizardStepVM step)
    {
        step.StepTransitionRequested += OnStepTransitionRequested;
        step.CloseWizardRequested += OnCloseWizardRequested;
    }

    private void RemoveEventHandlers(IWizardStepVM step)
    {
        step.StepTransitionRequested -= OnStepTransitionRequested;
        step.CloseWizardRequested -= OnCloseWizardRequested;
    }

    private void OnStepTransitionRequested(object sender, StepTransitionEventArgs args)
    {
        RemoveEventHandlers(CurrentStep);

        AddEventHandlers(args.Step);
        StartIfAutomatic(args.Step);
        CurrentStep = args.Step;
    }

    private void OnCloseWizardRequested(object _, EventArgs __)
    {
        _ownerWindow.Close();
    }

    private static void StartIfAutomatic(IWizardStepVM step)
    {
        if (step is IAutomaticWizardStepVM automaticStep)
        {
            automaticStep.Start();
        }
    }
}