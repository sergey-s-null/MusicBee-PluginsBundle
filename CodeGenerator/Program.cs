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
    }
}