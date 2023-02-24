using Autofac;
using Module.InboxAdder.Services;

namespace Module.InboxAdder;

public sealed class InboxAdderModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<InboxAddService>()
            .As<IInboxAddService>()
            .SingleInstance();
    }
}