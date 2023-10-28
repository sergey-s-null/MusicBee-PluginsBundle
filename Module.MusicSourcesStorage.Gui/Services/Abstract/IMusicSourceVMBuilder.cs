using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IMusicSourceVMBuilder
{
    [Obsolete("Gui -> Database is forbidden")]
    IMusicSourceVM Build(MusicSourceModel musicSource);
}