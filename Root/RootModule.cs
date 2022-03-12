using Ninject.Modules;
using Root.Services;
using Root.Services.Abstract;

namespace Root
{
    public class RootModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IResourceManager>()
                .To<ResourceManager>()
                .InSingletonScope();
        }
    }
}