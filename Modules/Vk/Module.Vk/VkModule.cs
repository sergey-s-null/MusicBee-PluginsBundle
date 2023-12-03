using Autofac;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using GuiDIModule = Module.Vk.Gui.DIModule;
using LogicDIModule = Module.Vk.Logic.DIModule;

namespace Module.Vk;

public sealed class VkModule : Autofac.Module
{
    private readonly bool _withAudioBypass;

    public VkModule(bool withAudioBypass)
    {
        _withAudioBypass = withAudioBypass;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<GuiDIModule>();
        builder.RegisterModule<LogicDIModule>();

        builder
            .Register(x => CreateVkApi())
            .As<IVkApi>()
            .SingleInstance();
    }

    private IVkApi CreateVkApi()
    {
        if (!_withAudioBypass)
        {
            return new VkApi();
        }

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddAudioBypass();
        return new VkApi(serviceCollection);
    }
}