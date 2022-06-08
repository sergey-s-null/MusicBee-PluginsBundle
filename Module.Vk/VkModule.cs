using Microsoft.Extensions.DependencyInjection;
using Module.Vk.Settings;
using Ninject.Modules;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;

namespace Module.Vk
{
    public class VkModule : NinjectModule
    {
        private readonly bool _withAudioBypass;

        public VkModule(bool withAudioBypass)
        {
            _withAudioBypass = withAudioBypass;
        }

        public override void Load()
        {
            Bind<IVkSettings>()
                .To<VkSettings>()
                .InSingletonScope();

            // Services
            Bind<IVkApi>()
                .ToMethod(_ => CreateVkApi())
                .InSingletonScope();
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