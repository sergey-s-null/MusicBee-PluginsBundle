﻿namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IImageFileVM : IFileVM
{
    public bool IsCover { get; }
    // todo если изображение выбрано как обложка, то должно хранить это само изображение
}