using Module.InboxAdder.Factories;
using Module.InboxAdder.Services;
using Ninject.Extensions.Factory;
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

            Bind<ITagsCopyService>()
                .To<TagsCopyService>()
                .InSingletonScope();

            Bind<IFileByIndexSelectDialogFactory>()
                .ToFactory();
        }
    }
}