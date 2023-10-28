using Autofac.Features.AttributeFilters;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourcesWindowVM : IMusicSourcesWindowVM
{
    // todo add loading placeholder

    public IList<IMusicSourceVM> MusicSources { get; private set; }

    public IMusicSourceVM? SelectedMusicSource { get; set; }

    private readonly IMusicSourceVMBuilder _musicSourceVMBuilder;

    public MusicSourcesWindowVM(
        [KeyFilter(ConnectionState.Connected)]
        IMusicSourceVMBuilder musicSourceVMBuilder,
        IMusicSourcesStorageService storageService)
    {
        _musicSourceVMBuilder = musicSourceVMBuilder;

        MusicSources = new List<IMusicSourceVM>();

        storageService.GetMusicSourcesAsync()
            .ContinueWith(OnMusicSourcesLoaded);
    }

    private void OnMusicSourcesLoaded(Task<IReadOnlyList<MusicSource>> task)
    {
        var sources = task.Result;

        MusicSources = sources
            .Select(x => _musicSourceVMBuilder.Build(x))
            .ToList();
    }
}