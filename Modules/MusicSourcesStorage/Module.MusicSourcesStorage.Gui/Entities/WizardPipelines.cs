﻿using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class WizardPipelines : IWizardPipelines
{
    private readonly Lazy<IWizardStepDescriptor> _addingVkPostWithArchivePipeline =
        new(CreateAddingVkPostWithArchivePipeline);

    private readonly Lazy<IWizardStepDescriptor> _addTorrentPipeline = new(CreateAddTorrentPipeline);

    private readonly Lazy<IWizardStepDescriptor> _editMusicSourceAdditionalInfoPipeline =
        new(CreateEditMusicSourceAdditionalInfoPipeline);

    public IWizardStepDescriptor GetRootDescriptor(WizardType wizardType)
    {
        return wizardType switch
        {
            WizardType.AddVkPostWithArchive => _addingVkPostWithArchivePipeline.Value,
            WizardType.AddTorrent => _addTorrentPipeline.Value,
            WizardType.EditMusicSourceAdditionalInfo => _editMusicSourceAdditionalInfoPipeline.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(wizardType), wizardType, "Unknown wizard type.")
        };
    }

    private static IWizardStepDescriptor CreateAddingVkPostWithArchivePipeline()
    {
        #region Steps declaration

        var step1 = new WizardStepDescriptor(StepType.SelectVkPost);
        var step1Error = new WizardStepDescriptor(StepType.Error);

        var step2 = new WizardStepDescriptor(StepType.ReceiveVkPostDocumentsStepVM);
        var step2Error = new WizardStepDescriptor(StepType.Error);

        var step3 = new WizardStepDescriptor(StepType.SelectDocumentFromVkPost);

        var step4 = new WizardStepDescriptor(StepType.DownloadAndIndexArchive);
        var step4Error = new WizardStepDescriptor(StepType.Error);

        var step5 = new WizardStepDescriptor(StepType.IndexingResult);

        var step6 = new WizardStepDescriptor(StepType.EditMusicSourceAdditionalInfo);

        var step7 = new WizardStepDescriptor(StepType.AddVkPostWithArchiveSourceToDatabase);
        var step7Error = new WizardStepDescriptor(StepType.Error);
        var step7Success = new SuccessStepDescriptor("Music source added to database");

        #endregion

        #region Steps initialization

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
        step6.NextStepDescriptor = step7;
        step6.CustomNextCommandName = "Add";

        step7.PreviousStepDescriptor = step6;
        step7.NextStepDescriptor = step7Success;
        step7.ErrorStepDescriptor = step7Error;

        step7Error.PreviousStepDescriptor = step5;

        step7Success.CanSafelyCloseWizard = true;
        step7Success.CustomCloseWizardCommandName = "Done";

        #endregion

        return step1;
    }

    private static IWizardStepDescriptor CreateAddTorrentPipeline()
    {
        #region Steps declaration

        var step1 = new WizardStepDescriptor(StepType.SelectTorrentFile);
        var step1Error = new WizardStepDescriptor(StepType.Error);

        var step2 = new WizardStepDescriptor(StepType.IndexTorrent);
        var step2Error = new WizardStepDescriptor(StepType.Error);

        var step3 = new WizardStepDescriptor(StepType.IndexingResult);

        var step4 = new WizardStepDescriptor(StepType.EditMusicSourceAdditionalInfo);

        var step5 = new WizardStepDescriptor(StepType.AddTorrentSourceToDatabase);
        var step5Error = new WizardStepDescriptor(StepType.Error);
        var step5Success = new SuccessStepDescriptor("Torrent source added to database");

        #endregion

        #region Steps initialization

        step1.NextStepDescriptor = step2;
        step1.ErrorStepDescriptor = step1Error;
        step1.CanSafelyCloseWizard = true;

        step1Error.PreviousStepDescriptor = step1;

        step2.NextStepDescriptor = step3;
        step2.PreviousStepDescriptor = step1;
        step2.ErrorStepDescriptor = step2Error;

        step2Error.PreviousStepDescriptor = step1;

        step3.NextStepDescriptor = step4;
        step3.PreviousStepDescriptor = step1;

        step4.NextStepDescriptor = step5;
        step4.PreviousStepDescriptor = step3;

        step5.PreviousStepDescriptor = step4;
        step5.ErrorStepDescriptor = step5Error;
        step5.NextStepDescriptor = step5Success;
        step5.CustomNextCommandName = "Add";

        step5Error.PreviousStepDescriptor = step5;

        step5Success.CanSafelyCloseWizard = true;
        step5Success.CustomCloseWizardCommandName = "Done";

        #endregion

        return step1;
    }

    private static IWizardStepDescriptor CreateEditMusicSourceAdditionalInfoPipeline()
    {
        var step1 = new WizardStepDescriptor(StepType.ReceiveMusicSourceAdditionalInfo);
        var step1Error = new WizardStepDescriptor(StepType.Error);

        var step2 = new WizardStepDescriptor(StepType.EditMusicSourceAdditionalInfo);

        var step3 = new WizardStepDescriptor(StepType.UpdateMusicSourceAdditionalInfoInDatabase);
        var step3Error = new WizardStepDescriptor(StepType.Error);

        var step4 = new WizardStepDescriptor(StepType.RetargetSourceFiles);
        var step4Error = new WizardStepDescriptor(StepType.Error);

        step1.NextStepDescriptor = step2;
        step1.ErrorStepDescriptor = step1Error;

        step2.NextStepDescriptor = step3;
        step2.CustomNextCommandName = "Save";

        step3.NextStepDescriptor = step4;
        step3.PreviousStepDescriptor = step2;
        step3.ErrorStepDescriptor = step3Error;

        step3Error.PreviousStepDescriptor = step2;

        step4.PreviousStepDescriptor = step2;
        step4.ErrorStepDescriptor = step4Error;

        step4Error.PreviousStepDescriptor = step2;

        return step1;
    }
}