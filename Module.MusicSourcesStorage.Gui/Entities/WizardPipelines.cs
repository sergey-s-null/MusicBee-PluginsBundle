using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class WizardPipelines : IWizardPipelines
{
    public IWizardStepDescriptor AddingVkPostWithArchivePipeline => _addingVkPostWithArchivePipeline.Value;

    private readonly Func<StepType, WizardStepDescriptor> _descriptorFactory;

    private readonly Lazy<IWizardStepDescriptor> _addingVkPostWithArchivePipeline;

    public WizardPipelines(Func<StepType, WizardStepDescriptor> descriptorFactory
    )
    {
        _descriptorFactory = descriptorFactory;
        _addingVkPostWithArchivePipeline = new Lazy<IWizardStepDescriptor>(CreateAddingVkPostWithArchivePipeline);
    }

    private IWizardStepDescriptor CreateAddingVkPostWithArchivePipeline()
    {
        var step1 = _descriptorFactory(StepType.SelectVkPost);
        var step1Error = _descriptorFactory(StepType.Error);

        var step2 = _descriptorFactory(StepType.ReceiveVkPostDocumentsStepVM);
        var step2Error = _descriptorFactory(StepType.Error);

        var step3 = _descriptorFactory(StepType.SelectDocumentFromVkPost);

        var step4 = _descriptorFactory(StepType.DownloadAndIndexArchive);
        var step4Error = _descriptorFactory(StepType.Error);

        var step5 = _descriptorFactory(StepType.IndexingResult);

        // todo? add step for additional information

        var step6 = _descriptorFactory(StepType.AddMusicSourceToDatabase);
        var step6Error = _descriptorFactory(StepType.Error);
        var step6Success = _descriptorFactory(StepType.SuccessResult);

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