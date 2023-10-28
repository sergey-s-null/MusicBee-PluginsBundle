using AutoMapper;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourcesWindowVM : IMusicSourcesWindowVM
{
    // todo add loading placeholder
    
    public IList<IMusicSourceVM> MusicSources { get; private set; }

    public IMusicSourceVM? SelectedMusicSource { get; set; }

    private readonly IMapper _mapper;

    public MusicSourcesWindowVM(
        IMusicSourcesStorage musicSourcesStorage,
        IMapper mapper)
    {
        _mapper = mapper;

        MusicSources = new List<IMusicSourceVM>();

        musicSourcesStorage.GetAllAsync()
            .ContinueWith(OnMusicSourcesLoaded);
    }

    private void OnMusicSourcesLoaded(Task<IReadOnlyList<MusicSource>> task)
    {
        var sources = task.Result;

        MusicSources = sources
            // todo configure mapper
            .Select(x => _mapper.Map<IMusicSourceVM>(x))
            .ToList();
    }
}