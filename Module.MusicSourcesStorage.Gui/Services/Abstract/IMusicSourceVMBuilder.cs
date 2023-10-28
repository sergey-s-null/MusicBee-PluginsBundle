using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IMusicSourceVMBuilder
{
    IMusicSourceVM Build(MusicSource musicSource);
}