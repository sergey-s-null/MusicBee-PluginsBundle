using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class WizardPipelines : IWizardPipelines
{
    private readonly IWizardStepDescriptorFactory _stepDescriptorFactory;
    private readonly SuccessResultStepVMFactory _successResultStepVMFactory;

    private readonly Lazy<IWizardStepDescriptor> _addingVkPostWithArchivePipeline;

    public WizardPipelines(
        IWizardStepDescriptorFactory stepDescriptorFactory,
        SuccessResultStepVMFactory successResultStepVMFactory)
    {
        _stepDescriptorFactory = stepDescriptorFactory;
        _successResultStepVMFactory = successResultStepVMFactory;

        _addingVkPostWithArchivePipeline = new Lazy<IWizardStepDescriptor>(CreateAddingVkPostWithArchivePipeline);
    }

    public IWizardStepDescriptor GetRootDescriptor(WizardType wizardType)
    {
        return wizardType switch
        {
            WizardType.AddingVkPostWithArchive => _addingVkPostWithArchivePipeline.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(wizardType), wizardType, "Unknown wizard type.")
        };
    }

    private IWizardStepDescriptor CreateAddingVkPostWithArchivePipeline()
    {
        var step1 = _stepDescriptorFactory.Create(StepType.SelectVkPost);
        var step1Error = _stepDescriptorFactory.Create(StepType.Error);

        var step2 = _stepDescriptorFactory.Create(StepType.ReceiveVkPostDocumentsStepVM);
        var step2Error = _stepDescriptorFactory.Create(StepType.Error);

        var step3 = _stepDescriptorFactory.Create(StepType.SelectDocumentFromVkPost);

        var step4 = _stepDescriptorFactory.Create(StepType.DownloadAndIndexArchive);
        var step4Error = _stepDescriptorFactory.Create(StepType.Error);

        var step5 = _stepDescriptorFactory.Create(StepType.IndexingResult);

        // todo? add step for additional information

        var step6 = _stepDescriptorFactory.Create(StepType.AddMusicSourceToDatabase);
        var step6Error = _stepDescriptorFactory.Create(StepType.Error);
        var step6Success = _stepDescriptorFactory.Create(
            () => _successResultStepVMFactory("Music source added to database")
        );

        step1.NextStepDescriptor = step2;
        step1.ErrorStepDescriptor = step1Error;
        step1.CanSafelyCloseWizard = true;

        step1Error.PreviousStepDescriptor = step1;

        step2.PreviousStepDescriptor = step1;
        step2.NextStepDescriptor = step3;
        step2.ErrorStepDescriptor = step2Error;

        step2Error.PreviousStepDescriptor = step1;

        step3.PreviousStepDescriptor = step1;
        step3.NextStepDescriptor = step4;

        step4.PreviousStepDescriptor = step3;
        step4.NextStepDescriptor = step5;
        step4.ErrorStepDescriptor = step4Error;

        step4Error.PreviousStepDescriptor = step3;

        step5.PreviousStepDescriptor = step3;
        step5.NextStepDescriptor = step6;

        step6.PreviousStepDescriptor = step5;
        step6.NextStepDescriptor = step6Success;
        step6.ErrorStepDescriptor = step6Error;

        step6Error.PreviousStepDescriptor = step5;

        step6Success.CanSafelyCloseWizard = true;
        step6Success.CustomCloseWizardCommandName = "Done";

        return step1;
    }
}