using Ninject.Modules;
using Root.Services;
using Root.Services.Abstract;

namespace Root
{
    public sealed class RootModule : NinjectModule
    {
        public override void Load()
        {
            // Services
            Bind<IResourceManager>()
                .To<ResourceManager>()
                .InSingletonScope();
            Bind<ISettingsJsonLoader>()
                .To<SettingsJsonLoader>()
                .InSingletonScope();
        }
    }
}