using Ninject.Modules;

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