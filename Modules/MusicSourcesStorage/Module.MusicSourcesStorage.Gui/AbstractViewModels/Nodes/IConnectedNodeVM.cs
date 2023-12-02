namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedNodeVM : 
    INodeVM,
    IProcessableVM,
    IDownloadableVM,
    IDeletableVM
{
    // todo add TargetPath
    // string TargetPath { get; }
}