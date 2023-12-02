using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class IndexingResultStepVM : IIndexingResultStepVM
{
    public bool IsValidState => true;

    public INodesHierarchyVM<INodeVM> Items { get; }

    public IndexingResultStepVM(
        IIndexedFilesContext context,
        INodesHierarchyVMBuilder nodesHierarchyVMBuilder)
    {
        context.ValidateHasIndexedFiles();

        Items = nodesHierarchyVMBuilder.Build(context.IndexedFiles!);
    }

    public StepResult Confirm()
    {
        return StepResult.Success;
    }
}