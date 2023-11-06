using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedImageFileVM : ImageFileVM, IConnectedImageFileVM
{
    public bool IsProcessing => throw new NotImplementedException();

    public bool CanDownload => throw new NotImplementedException();
    public bool IsDownloaded => throw new NotImplementedException();

    public bool CanDelete => throw new NotImplementedException();
    public bool IsDeleted => throw new NotImplementedException();

    public bool IsCover => throw new NotImplementedException();

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand SelectAsCover => _selectAsCoverCmd ??= new RelayCommand(SelectAsCoverCmd);

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _selectAsCoverCmd;

    public ConnectedImageFileVM(string path) : base(path)
    {
    }

    private void DownloadCmd()
    {
        throw new NotImplementedException();
    }

    private void DeleteCmd()
    {
        throw new NotImplementedException();
    }

    private void SelectAsCoverCmd()
    {
        throw new NotImplementedException();
    }
}