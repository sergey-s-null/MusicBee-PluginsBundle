using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.Views;
using Module.MusicSourcesStorage.Services.Abstract;

namespace Module.MusicSourcesStorage.Services;

public sealed class MusicSourcesStorageModuleActions : IMusicSourcesStorageModuleActions
{
    private readonly Func<MusicSourcesWindow> _musicSourcesWindowsFactory;
    private readonly IWizardService _wizardService;

    public MusicSourcesStorageModuleActions(
        Func<MusicSourcesWindow> musicSourcesWindowsFactory,
        IWizardService wizardService)
    {
        _musicSourcesWindowsFactory = musicSourcesWindowsFactory;
        _wizardService = wizardService;
    }

    public void ShowMusicSources()
    {
        _musicSourcesWindowsFactory().ShowDialog();
    }

    public void AddVkPostWithArchiveSource()
    {
        _wizardService.AddVkPostWithArchiveSource();
    }
}