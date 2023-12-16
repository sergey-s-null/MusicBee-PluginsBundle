using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Exceptions;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IHierarchyBuilder<TValue, TPathElement>
{
    /// <exception cref="LeafHasNodeNameException">
    /// Configuration is set to <see cref="LeafHasNodeNameResolutionMode"/>.<see cref="LeafHasNodeNameResolutionMode.ThrowException"/>
    /// and found leaf with node name.
    /// </exception>
    /// <exception cref="LeavesDuplicationException">
    /// Configuration is set to <see cref="LeavesDuplicationResolutionMode"/>.<see cref="LeavesDuplicationResolutionMode.ThrowException"/>
    /// and found duplicated leaves.
    /// </exception>
    void Build(
        IReadOnlyList<TValue> values,
        out IReadOnlyList<INode<TValue, TPathElement>> rootNodes,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> rootLeaves
    );
}