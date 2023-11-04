using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedImageFileVM : ImageFileVM, IConnectedImageFileVM
{
    public bool IsDownloaded { get; private set; }
    public bool IsCover { get; private set; }

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand SelectAsCover => _selectAsCoverCmd ??= new RelayCommand(SelectAsCoverCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);

    private ICommand? _downloadCmd;
    private ICommand? _selectAsCoverCmd;
    private ICommand? _deleteCmd;

    public ConnectedImageFileVM(string path) : base(path)
    {
        // todo fill using api
        IsDownloaded = false;
        IsCover = false;
    }

    private void DownloadCmd()
    {
        throw new NotImplementedException();
    }

    private void SelectAsCoverCmd()
    {
        throw new NotImplementedException();
    }

    private void DeleteCmd()
    {
        throw new NotImplementedException();
    }
}