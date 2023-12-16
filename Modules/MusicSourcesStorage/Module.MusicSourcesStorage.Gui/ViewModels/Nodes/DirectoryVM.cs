using System.Collections.ObjectModel;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class DirectoryVM : IDirectoryVM
{
    public string Name { get; }
    public string Path { get; }

    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            if (value)
            {
                LoadChildNodes();
            }
            else
            {
                UnloadChildNodes();
            }

            _isExpanded = value;
        }
    }

    private bool _isExpanded;

    public IReadOnlyList<INodeVM> ChildNodes => _childNodesCollection;
    private readonly ObservableCollection<INodeVM> _childNodesCollection = new() { null! }; // todo use placeholder

    private readonly Lazy<IReadOnlyList<INodeVM>> _childNodes;

    private bool _isChildNodesLoaded;

    public DirectoryVM(string path, Func<IReadOnlyList<INodeVM>> childNodesFactory)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
        _childNodes = new Lazy<IReadOnlyList<INodeVM>>(childNodesFactory);
    }

    private void LoadChildNodes()
    {
        if (_isChildNodesLoaded)
        {
            return;
        }

        _childNodesCollection.Clear();
        foreach (var childNode in _childNodes.Value)
        {
            _childNodesCollection.Add(childNode);
        }

        _isChildNodesLoaded = true;
    }

    private void UnloadChildNodes()
    {
        if (!_isChildNodesLoaded)
        {
            return;
        }

        _childNodesCollection.Clear();
        _childNodesCollection.Add(null!);

        _isChildNodesLoaded = false;
    }
}