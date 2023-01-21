using System;
using Autofac;
using CodeGenerator.Builders;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Enums;

namespace CodeGenerator
{
    public static class ApplicationContainer
    {
        public static IContainer Create(ServiceImplMode serviceImplMode)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<ServiceBuilder>()
                .As<IServiceBuilder>()
                .SingleInstance();
            builder
                .RegisterType<HardcodedServiceBuilderParameters>()
                .As<IServiceBuilderParameters>()
                .SingleInstance();
            builder
                .RegisterType<MessageTypesBuilder>()
                .As<IMessageTypesBuilder>()
                .SingleInstance();
            builder
                .RegisterType<CommonLinesBuilder>()
                .As<ICommonLinesBuilder>()
                .SingleInstance();

            switch (serviceImplMode)
            {
                case ServiceImplMode.TaskFromResult:
                    builder
                        .RegisterType<TaskFromResultMethodBuilder>()
                        .As<IMethodBuilder>()
                        .SingleInstance();
                    break;
                case ServiceImplMode.WrapWithTaskRun:
                    builder
                        .RegisterType<TaskRunWrappedMethodBuilder>()
                        .As<IMethodBuilder>()
                        .SingleInstance();
                    break;
                default:
                    throw new ArgumentException("Argument is out of range.", nameof(serviceImplMode));
            }

            builder
                .RegisterType<InterfaceBuilder>()
                .As<IInterfaceBuilder>();
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