using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.AudioSourcesComparer.GUI.Factories;
using Module.AudioSourcesComparer.GUI.ViewModels;
using Module.AudioSourcesComparer.Services;
using Module.AudioSourcesComparer.Services.Abstract;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Module.AudioSourcesComparer
{
    public class AudioSourcesComparerModule : NinjectModule
    {
        public override void Load()
        {
            // Services
            Bind<IVkToLocalComparerService>()
                .To<VkToLocalComparerService>()
                .InSingletonScope();

            // ViewModels
            Bind<IVkToLocalComparerWindowVM>()
                .To<VkToLocalComparerWindowVM>();

            // Factories
            Bind<IVkToLocalComparerWindowFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}