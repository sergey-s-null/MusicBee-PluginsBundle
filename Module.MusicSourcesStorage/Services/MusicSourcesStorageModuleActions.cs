using System.Windows.Threading;
using Autofac;
using Module.Core.Enums;
using Module.Core.Services;
using Module.Core.Services.Abstract;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.Views;
using Module.MusicSourcesStorage.Services.Abstract;

namespace Module.MusicSourcesStorage.Services;

public sealed class MusicSourcesStorageModuleActions : IMusicSourcesStorageModuleActions
{
    private readonly ILifetimeScope _lifetimeScope;
    private readonly IWizardService _wizardService;

    public MusicSourcesStorageModuleActions(
        ILifetimeScope lifetimeScope,
        IWizardService wizardService)
    {
        _lifetimeScope = lifetimeScope;
        _wizardService = wizardService;
    }

    public void ShowMusicSources()
    {
        using var dispatcherScope = CreateUiDispatcherScope();
        var musicSourcesWindowFactory = dispatcherScope.Resolve<Func<MusicSourcesWindow>>();
        musicSourcesWindowFactory().ShowDialog();
    }

    public void AddVkPostWithArchiveSource()
    {
        _wizardService.AddVkPostWithArchiveSource();
    }

    private ILifetimeScope CreateUiDispatcherScope()
    {
        return _lifetimeScope.BeginLifetimeScope(
            Scope.UiDispatcher,
            builder => builder
                .RegisterInstance(new UiDispatcherProvider(Dispatcher.CurrentDispatcher))
                .As<IUiDispatcherProvider>()
        );
    }
}