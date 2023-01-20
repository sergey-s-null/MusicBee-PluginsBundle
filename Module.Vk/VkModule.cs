using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.GUI.ViewModels;
using Module.Vk.Settings;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;

namespace Module.Vk
{
    public sealed class VkModule : Autofac.Module
    {
        private readonly bool _withAudioBypass;

        public VkModule(bool withAudioBypass)
        {
            _withAudioBypass = withAudioBypass;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<VkSettings>()
                .As<IVkSettings>()
                .SingleInstance();

            builder
                .Register(x => CreateVkApi())
                .As<IVkApi>()
                .SingleInstance();

            builder
                .RegisterType<VkSettingsVM>()
                .As<IVkSettingsVM>();
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
}