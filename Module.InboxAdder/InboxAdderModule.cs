using Module.InboxAdder.Services;
using Ninject.Modules;

namespace Module.InboxAdder
{
    public class InboxAdderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInboxAddService>()
                .To<InboxAddService>()
                .InSingletonScope();
        }
    }
}