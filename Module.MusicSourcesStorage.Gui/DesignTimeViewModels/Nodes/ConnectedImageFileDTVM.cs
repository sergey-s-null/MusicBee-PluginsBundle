﻿using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedImageFileDTVM : ImageFileDTVM, IConnectedImageFileVM
{
    public bool CanDownload => !IsDownloaded;
    public bool IsDownloaded => true;

    public bool IsCover => false;

    public ICommand Download => null!;
    public ICommand SelectAsCover => null!;
    public ICommand Delete => null!;

    public ConnectedImageFileDTVM()
    {
    }

    public ConnectedImageFileDTVM(string path, bool isCover) : base(path)
    {
    }
}