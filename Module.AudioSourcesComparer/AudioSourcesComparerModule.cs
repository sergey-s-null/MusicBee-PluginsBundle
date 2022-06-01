using Module.AudioSourcesComparer.GUI.Factories;
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
            Bind<IVkToLocalComparerService>()
                .To<VkToLocalComparerService>()
                .InSingletonScope();
            Bind<IVkToLocalComparerWindowFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}