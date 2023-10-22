using AutoMapper;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectDocumentFromVkPostStepVM : ISelectDocumentFromVkPostStepVM
{
    public bool IsValidState { get; private set; }

    public ulong PostOwnerId { get; }
    public ulong PostId { get; }

    public IReadOnlyList<IVkDocumentVM> Documents { get; }

    [OnChangedMethod(nameof(UpdateValidityState))]
    public IVkDocumentVM? SelectedDocument { get; set; }

    private readonly IVkPostWithArchiveMusicSourceBuilder _musicSourceBuilder;
    private readonly IMapper _mapper;

    private readonly IReadOnlyDictionary<IVkDocumentVM, VkDocument> _documentsMap;

    public SelectDocumentFromVkPostStepVM(
        IReadOnlyList<VkDocument> documents,
        IVkPostWithArchiveMusicSourceBuilder musicSourceBuilder,
        IMapper mapper)
    {
        _musicSourceBuilder = musicSourceBuilder;
        _mapper = mapper;

        ValidateBuilderState();

        PostOwnerId = _musicSourceBuilder.PostId!.OwnerId;
        PostId = _musicSourceBuilder.PostId!.LocalId;

        MapDocuments(documents, out _documentsMap, out var viewModels, out var selectedViewModel);
        Documents = viewModels;
        SelectedDocument = selectedViewModel;

        UpdateValidityState();
    }

    public StepResult Confirm()
    {
        if (!IsValidState || SelectedDocument is null)
        {
            throw new InvalidOperationException();
        }

        _musicSourceBuilder.Document = _documentsMap[SelectedDocument];

        return StepResult.Success;
    }

    private void ValidateBuilderState()
    {
        if (_musicSourceBuilder.PostId is null)
        {
            throw new InvalidOperationException(
                "Music source builder has invalid state. " +
                "PostId is null."
            );
        }
    }

    private void MapDocuments(
        IReadOnlyList<VkDocument> documents,
        out IReadOnlyDictionary<IVkDocumentVM, VkDocument> map,
        out IReadOnlyList<IVkDocumentVM> viewModels,
        out IVkDocumentVM? selectedViewModel)
    {
        var mapInternal = new Dictionary<IVkDocumentVM, VkDocument>();
        var viewModelsInternal = new List<IVkDocumentVM>();
        selectedViewModel = null;

        foreach (var document in documents)
        {
            var viewModel = _mapper.Map<IVkDocumentVM>(document);
            mapInternal[viewModel] = document;
            viewModelsInternal.Add(viewModel);

            if (_musicSourceBuilder.Document == document)
            {
                selectedViewModel = viewModel;
            }
        }

        map = mapInternal;
        viewModels = viewModelsInternal;
    }

    private void UpdateValidityState()
    {
        IsValidState = SelectedDocument is not null;
    }
}