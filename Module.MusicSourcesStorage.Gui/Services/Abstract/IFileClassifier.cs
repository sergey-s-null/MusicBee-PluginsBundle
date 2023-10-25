using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IFileClassifier
{
    FileType Classify(MusicSourceFile file);
}