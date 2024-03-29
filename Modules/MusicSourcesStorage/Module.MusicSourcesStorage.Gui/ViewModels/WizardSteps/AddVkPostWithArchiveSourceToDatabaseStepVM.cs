﻿using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class AddVkPostWithArchiveSourceToDatabaseStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }
    public override IProgressVM? Progress { get; protected set; }

    private readonly IAddingVkPostWithArchiveContext _context;
    private readonly IEditMusicSourceAdditionalInfoContext _additionalInfoContext;
    private readonly IIndexedFilesContext _indexedFilesContext;
    private readonly IMusicSourcesStorageService _storageService;

    public AddVkPostWithArchiveSourceToDatabaseStepVM(
        IAddingVkPostWithArchiveContext context,
        IEditMusicSourceAdditionalInfoContext additionalInfoContext,
        IIndexedFilesContext indexedFilesContext,
        IMusicSourcesStorageService storageService)
        : base(context)
    {
        _context = context;
        _additionalInfoContext = additionalInfoContext;
        _indexedFilesContext = indexedFilesContext;
        _storageService = storageService;

        ValidateContext();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Adding music source to database";

        var source = VkPostWithArchiveSource.New(
            _additionalInfoContext.EditedAdditionalInfo ?? _additionalInfoContext.InitialAdditionalInfo!,
            _indexedFilesContext.IndexedFiles!,
            new VkPost(_context.PostId!),
            _context.SelectedDocument!
        );
        var result = await _storageService.AddMusicSourceAsync(source, token);

        _context.Result = result;

        return StepResult.Success;
    }

    private void ValidateContext()
    {
        _context.ValidateHasPostId();
        _context.ValidateHasSelectedDocument();
        _indexedFilesContext.ValidateHasIndexedFiles();
        _additionalInfoContext.ValidateHasInitialAdditionalInfo();
    }
}