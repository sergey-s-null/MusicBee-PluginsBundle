using System;
using MBApiProtoGenerator.Builders;
using MBApiProtoGenerator.Builders.Abstract;
using MBApiProtoGenerator.Builders.ServiceImplBuilder;
using MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract;
using MBApiProtoGenerator.Enums;
using Ninject;
using Ninject.Syntax;

namespace MBApiProtoGenerator
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
                .Bind<IParameters>()
                .To<HardcodedParameters>()
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

            return kernel;
        }
    }
}