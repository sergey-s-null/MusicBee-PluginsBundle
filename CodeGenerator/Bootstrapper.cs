using System;
using CodeGenerator.Builders;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Enums;
using Ninject;
using Ninject.Syntax;

namespace CodeGenerator
{
    public static class Bootstrapper
    {
        public static IResolutionRoot GetKernel(ServiceImplMode serviceImplMode)
        {
            var kernel = new StandardKernel();
            kernel
                .Bind<IServiceBuilder>()
                .To<ServiceBuilder>()
                .InSingletonScope();
            kernel
                .Bind<IServiceBuilderParameters>()
                .To<HardcodedServiceBuilderParameters>()
                .InSingletonScope();
            kernel
                .Bind<IMessageTypesBuilder>()
                .To<MessageTypesBuilder>()
                .InSingletonScope();
            kernel
                .Bind<ICommonLinesBuilder>()
                .To<CommonLinesBuilder>()
                .InSingletonScope();

            switch (serviceImplMode)
            {
                case ServiceImplMode.TaskFromResult:
                    kernel
                        .Bind<IMethodBuilder>()
                        .To<TaskFromResultMethodBuilder>()
                        .InSingletonScope();
                    break;
                case ServiceImplMode.WrapWithTaskRun:
                    kernel
                        .Bind<IMethodBuilder>()
                        .To<TaskRunWrappedMethodBuilder>()
                        .InSingletonScope();
                    break;
                default:
                    throw new ArgumentException("Argument is out of range.", nameof(serviceImplMode));
            }

            kernel
                .Bind<IInterfaceBuilder>()
                .To<InterfaceBuilder>();
            kernel
                .Bind<IMethodDefinitionBuilder>()
                .To<MethodDefinitionBuilder>()
                .InSingletonScope();
            kernel
                .Bind<IClientWrapperBuilder>()
                .To<ClientWrapperBuilder>()
                .InSingletonScope();
            kernel
                .Bind<IMemoryContainerWrapperBuilder>()
                .To<MemoryContainerWrapperBuilder>();

            return kernel;
        }
    }
}