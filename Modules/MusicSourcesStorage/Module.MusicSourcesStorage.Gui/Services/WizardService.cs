using Autofac;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.Views;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.Vk.Gui.Services.Abstract;
using Module.Vk.Logic.Entities;
using Module.Vk.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class WizardService : IWizardService
{
    private readonly ILifetimeScope _lifetimeScope;
    private readonly IWizardPipelines _wizardPipelines;
    private readonly IVkApiProvider _vkApiProvider;

    public WizardService(
        ILifetimeScope lifetimeScope,
        IWizardPipelines wizardPipelines,
        IVkApiProvider vkApiProvider)
    {
        _lifetimeScope = lifetimeScope;
        _wizardPipelines = wizardPipelines;
        _vkApiProvider = vkApiProvider;
    }

    public MusicSource? AddVkPostWithArchiveSource()
    {
        if (!_vkApiProvider.TryProvideAuthorizedVkApi(out var vkApi))
        {
            return null;
        }

        var context = new AddingVkPostWithArchiveContext();

        var wizardScope = _lifetimeScope.BeginLifetimeScope(builder =>
        {
            builder
                .RegisterInstance(new AuthorizedVkApiProvider(vkApi))
                .As<IAuthorizedVkApiProvider>();
            RegisterRootDescriptor(builder, WizardType.AddVkPostWithArchive);
            builder
                .RegisterInstance(context)
                .As<IAddingVkPostWithArchiveContext>()
                .As<IIndexedFilesContext>()
                .As<IInitialMusicSourceAdditionalInfoContext>()
                .As<IEditMusicSourceAdditionalInfoContext>()
                .As<IWizardErrorContext>()
                .As<IWizardResultContext<MusicSource>>()
                .SingleInstance();
        });

        var wizard = wizardScope.Resolve<Wizard>();
        wizard.ShowDialog();

        return context.Result;
    }

    public MusicSource? AddTorrentSource()
    {
        var context = new AddingTorrentContext();

        var wizardScope = _lifetimeScope.BeginLifetimeScope(builder =>
        {
            RegisterRootDescriptor(builder, WizardType.AddTorrent);
            builder
                .RegisterInstance(context)
                .As<ITorrentFileContext>()
                .As<IIndexedFilesContext>()
                .As<IInitialMusicSourceAdditionalInfoContext>()
                .As<IEditMusicSourceAdditionalInfoContext>()
                .As<IWizardErrorContext>()
                .As<IWizardResultContext<MusicSource>>()
                .SingleInstance();
        });

        var wizard = wizardScope.Resolve<Wizard>();
        wizard.ShowDialog();

        return context.Result;
    }

    public MusicSourceAdditionalInfo? EditMusicSourceAdditionalInfo(int musicSourceId)
    {
        var context = new EditMusicSourceAdditionalInfoContext(musicSourceId);

        var wizardScope = _lifetimeScope.BeginLifetimeScope(builder =>
        {
            RegisterRootDescriptor(builder, WizardType.EditMusicSourceAdditionalInfo);
            builder
                .RegisterInstance(context)
                .As<IMusicSourceContext>()
                .As<IInitialMusicSourceAdditionalInfoContext>()
                .As<IEditMusicSourceAdditionalInfoContext>()
                .As<IWizardErrorContext>()
                .As<IWizardResultContext<MusicSourceAdditionalInfo>>();
        });

        var wizard = wizardScope.Resolve<Wizard>();
        wizard.ShowDialog();

        return context.Result;
    }

    private void RegisterRootDescriptor(ContainerBuilder builder, WizardType wizardType)
    {
        builder
            .RegisterInstance(_wizardPipelines.GetRootDescriptor(wizardType))
            .As<IWizardStepDescriptor>();
    }
}