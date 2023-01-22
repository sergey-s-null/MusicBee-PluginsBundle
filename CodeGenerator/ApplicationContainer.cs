using Autofac;
using CodeGenerator.Builders;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;

namespace CodeGenerator
{
    public static class ApplicationContainer
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<HardcodedServiceBuilderParameters>()
                .As<IServiceBuilderParameters>()
                .SingleInstance();
            builder
                .RegisterType<MessageTypesBuilder>()
                .As<IMessageTypesBuilder>()
                .SingleInstance();

            builder
                .RegisterType<MethodDefinitionBuilder>()
                .As<IMethodDefinitionBuilder>()
                .SingleInstance();
            builder
                .RegisterType<ClientWrapperBuilder>()
                .As<IClientWrapperBuilder>()
                .SingleInstance();
            builder
                .RegisterType<MemoryContainerWrapperBuilder>()
                .As<IMemoryContainerWrapperBuilder>();

            return builder.Build();
        }
    }
}