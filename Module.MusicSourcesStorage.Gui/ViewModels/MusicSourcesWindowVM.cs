using System.Collections.ObjectModel;
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

    public IList<IMusicSourceVM> MusicSources { get; } = new ObservableCollection<IMusicSourceVM>();

    public IMusicSourceVM? SelectedMusicSource { get; set; }

    private readonly IMusicSourceVMBuilder _musicSourceVMBuilder;
    private readonly IMusicSourcesStorageService _storageService;

    public MusicSourcesWindowVM(
        [KeyFilter(ConnectionState.Connected)] IMusicSourceVMBuilder musicSourceVMBuilder,
        IMusicSourcesStorageService storageService)
    {
        _musicSourceVMBuilder = musicSourceVMBuilder;
        _storageService = storageService;

        LoadMusicSources();
    }

    private async void LoadMusicSources()
    {
        var musicSources = await _storageService.GetMusicSourcesAsync();

        foreach (var musicSource in musicSources)
        {
            MusicSources.Add(CreateMusicSourceVM(musicSource));
        }
    }

    private IMusicSourceVM CreateMusicSourceVM(MusicSource musicSource)
    {
        var viewModel = _musicSourceVMBuilder.Build(musicSource);
        viewModel.Deleted += (sender, _) => OnMusicSourceDeleted((IMusicSourceVM)sender);
        return viewModel;
    }

    private void OnMusicSourceDeleted(IMusicSourceVM musicSourceVM)
    {
        MusicSources.Remove(musicSourceVM);
    }
}