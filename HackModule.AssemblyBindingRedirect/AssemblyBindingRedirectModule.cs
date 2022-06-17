using HackModule.AssemblyBindingRedirect.Factories;
using HackModule.AssemblyBindingRedirect.Services;
using HackModule.AssemblyBindingRedirect.Services.Abstract;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace HackModule.AssemblyBindingRedirect
{
    public class AssemblyBindingRedirectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAssemblyResolver>()
                .To<AssemblyResolver>();

            Bind<IAssemblyResolverFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}