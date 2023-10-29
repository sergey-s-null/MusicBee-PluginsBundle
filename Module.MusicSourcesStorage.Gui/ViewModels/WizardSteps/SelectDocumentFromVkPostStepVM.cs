using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Entities;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectDocumentFromVkPostStepVM : ISelectDocumentFromVkPostStepVM
{
    public bool IsValidState { get; private set; }

    public long PostOwnerId { get; }
    public long PostId { get; }

    public IReadOnlyList<IVkDocumentVM> Documents { get; }

    [OnChangedMethod(nameof(UpdateValidityState))]
    public IVkDocumentVM? SelectedDocument { get; set; }

    private readonly IAddingVkPostWithArchiveContext _context;

    private readonly IReadOnlyDictionary<IVkDocumentVM, VkDocument> _documentsMap;

    public SelectDocumentFromVkPostStepVM(IAddingVkPostWithArchiveContext context)
    {
        _context = context;

        ValidateContext();

        PostOwnerId = _context.PostId!.OwnerId;
        PostId = _context.PostId!.LocalId;

        MapDocuments(_context.AttachedDocuments!, out _documentsMap, out var viewModels, out var selectedViewModel);
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

        _context.SelectedDocument = _documentsMap[SelectedDocument];

        return StepResult.Success;
    }

    private void ValidateContext()
    {
        _context.ValidateHasPostId();
        _context.ValidateHasAttachedDocuments();
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
            var viewModel = document.ToViewModel();
            mapInternal[viewModel] = document;
            viewModelsInternal.Add(viewModel);

            if (_context.SelectedDocument == document)
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