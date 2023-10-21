﻿using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public class MusicFileDTVM : IMusicFileVM
{
    public string Name { get; }
    public string Path { get; }
    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    public MusicFileDTVM() : this("path/to/indexed_file.jpg")
    {
    }

    public MusicFileDTVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}