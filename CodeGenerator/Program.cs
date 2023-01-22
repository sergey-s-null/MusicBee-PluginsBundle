using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using CodeGenerator.Builders;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Enums;
using CodeGenerator.Helpers;
using Microsoft.Build.Evaluation;

namespace CodeGenerator
{
    internal class Program
    {
        private const string ServiceName = "MusicBeeApiService";
        private const string ReturnParameterName = "result";

        private const ServiceGenerationMode GenerationMode = ServiceGenerationMode.SingleFile;
        private const string ModuleProjectPath = @"..\..\..\Module.RemoteMusicBeeApi";
        private const string ExportPathInsideModuleProject = "Protos";
        private const string ModuleCsProjFilePath = ModuleProjectPath + @"\Module.RemoteMusicBeeApi.csproj";
        private const string ConsoleTestsCsProjFilePath = @"..\..\..\ConsoleTests\ConsoleTests.csproj";
        private const string FromConsoleTestToModulePath = @"..\Module.RemoteMusicBeeApi";

        public static void Main(string[] args)
        {
            var container = ApplicationContainer.Create(ServiceImplMode.WrapWithTaskRun);

            var baseMethods = GetMethodsDefinition(MethodNamesWithoutRestrictions);
            var extendedMethods = GetMethodsDefinition(ExtendedMethodNames);
            var methodsExceptIgnored = GetMethodsDefinition(MethodNamesExceptIgnored);

            AddProtobufToModuleCsProj(baseMethods);
            AddProtobufToConsoleTestsCsProj(baseMethods);

            GenerateServiceImpl(container, baseMethods);

            GenerateBaseInterface(container, baseMethods);
            GenerateExtendedInterface(container, extendedMethods);

            GenerateClientWrapper(container, baseMethods);
            GenerateMemoryContainerWrapper(container, methodsExceptIgnored);
        }

        private static void AddProtobufToModuleCsProj(IEnumerable<MethodDefinition> methods)
        {
            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(ModuleCsProjFilePath);

            var serviceFilePath = @$"{ExportPathInsideModuleProject}\{ServiceName}.proto";
            var filePaths = GenerationMode switch
            {
                ServiceGenerationMode.MessagesInSeparateFiles => methods
                    .Select(x => @$"{ExportPathInsideModuleProject}\{x.Name}.proto")
                    .Append(serviceFilePath),
                ServiceGenerationMode.SingleFile => new[] { serviceFilePath },
                _ => throw new ArgumentOutOfRangeException(nameof(GenerationMode), GenerationMode, null)
            };

            new CsProjProtobufBuilder(project)
                .RemoveAllProtobuf()
                .SetProtobufType(ProtobufType.Server)
                .AddProtobufItemGroup(filePaths)
                .SaveProject();
        }

        private static void AddProtobufToConsoleTestsCsProj(IEnumerable<MBApiMethodDefinition> methods)
        {
            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(ConsoleTestsCsProjFilePath);

            var builder = new CsProjProtobufBuilder(project)
                .RemoveAllProtobuf()
                .SetProtobufType(ProtobufType.Client);

            if (GenerationMode == ServiceGenerationMode.MessagesInSeparateFiles)
            {
                var messagesFilePaths = methods
                    .Select(x =>
                        Path.Combine(FromConsoleTestToModulePath, ExportPathInsideModuleProject, $"{x.Name}.proto"));
                builder
                    .AddProtobufItemGroup(messagesFilePaths)
                    .SetProtoRoot(FromConsoleTestToModulePath);
            }

            var serviceFilePath = Path.Combine(FromConsoleTestToModulePath, ExportPathInsideModuleProject,
                $"{ServiceName}.proto");
            builder
                .AddProtobufItemGroup(serviceFilePath)
                .SaveProject();
        }

        private static void GenerateServiceImpl(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            const string filePath = @"..\..\..\Module.RemoteMusicBeeApi\MusicBeeApiServiceImpl.cs";

            var lines = componentContext
                .Resolve<IServiceBuilder>()
                .GenerateServiceLines(methods);

            File.WriteAllLines(filePath, lines);
        }

        private static void GenerateBaseInterface(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> baseMethods)
        {
            const string baseFilePath = @"..\..\..\Root\MusicBeeApi\Abstract\IBaseMusicBeeApi.cs";

            var builder = componentContext.Resolve<IInterfaceBuilder>();
            builder.Namespace = "Root.MusicBeeApi.Abstract";
            builder.Name = "IBaseMusicBeeApi";
            var baseLines = builder
                .GenerateInterfaceLines(baseMethods);
            File.WriteAllLines(baseFilePath, baseLines);
        }

        private static void GenerateExtendedInterface(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> extendedMethods)
        {
            const string extendedFilePath = @"..\..\..\Root\MusicBeeApi\Abstract\IMusicBeeApi.cs";

            var builder = componentContext.Resolve<IInterfaceBuilder>();
            builder.ImportNamespaces = new[]
            {
                "System",
                "System.Drawing",
                "System.Threading",
                "System.Windows.Forms",
            };
            builder.Namespace = "Root.MusicBeeApi.Abstract";
            builder.Name = "IMusicBeeApi";
            builder.BaseInterface = "IBaseMusicBeeApi";

            var extendedLines = builder
                .GenerateInterfaceLines(extendedMethods);
            File.WriteAllLines(extendedFilePath, extendedLines);
        }

        private static void GenerateClientWrapper(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            const string wrapperFilePath = @"..\..\..\ConsoleTests\Services\MusicBeeApiClientWrapper.cs";

            var builder = componentContext
                .Resolve<IClientWrapperBuilder>();
            builder.ReturnVariableName = ReturnParameterName;

            var lines = builder
                .GenerateClientWrapperLines(methods);
            File.WriteAllLines(wrapperFilePath, lines);
        }

        private static void GenerateMemoryContainerWrapper(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            const string filePath = @"..\..\..\Root\MusicBeeApi\MusicBeeApiMemoryContainerWrapper.cs";

            var builder = componentContext.Resolve<IMemoryContainerWrapperBuilder>();
            builder.Namespace = "Root.MusicBeeApi";

            var lines = builder.GenerateMemoryContainerWrapperLines(methods);
            File.WriteAllLines(filePath, lines);
        }
    }
}