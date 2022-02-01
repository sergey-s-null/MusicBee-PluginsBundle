using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MBApiProtoGenerator.Builders;
using MBApiProtoGenerator.Models;
using Microsoft.Build.Evaluation;
using Root;
using Root.Helpers;

namespace MBApiProtoGenerator
{
    internal class Program
    {
        private const string ProjectPath = @"..\..\..\Module.RemoteMusicBeeApi";
        private const string ExportPathInsideProject = "Protos";
        
        private const string ServiceName = "MusicBeeApiService";
        
        private const string CsProjFilePath = ProjectPath + @"\Module.RemoteMusicBeeApi.csproj";

        private static readonly IReadOnlyCollection<string> FieldsToExport = new[]
        {
            "NowPlaying_GetArtistPicture",
        };
        
        public static void Main(string[] args)
        {
            var apiType = typeof(MusicBeeApiInterface);
            var methods = apiType
                .GetMembers()
                .Where(x => FieldsToExport.Contains(x.Name))
                .CastOrSkip<MemberInfo, FieldInfo>()
                .Select(Define)
                .ToReadOnlyCollection();

            new ProtoFilesBuilder()
                .SetPostfixes("_Request", "_Response")
                .SetExportPath(ProjectPath, ExportPathInsideProject)
                .SetReturnParameterName("result")
                .AddMethods(methods)
                .CreateMessagesProtoFiles()
                .CreateServiceProtoFile(ServiceName);
            
            AddProtobufToCsProj(methods);
        }
        
        private static void AddProtobufToCsProj(IEnumerable<MBApiMethodDefinition> methods)
        {
            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(CsProjFilePath);

            var filePaths = methods
                .Select(x => @$"{ExportPathInsideProject}\{x.Name}.proto")
                .Append(@$"{ExportPathInsideProject}\{ServiceName}.proto");

            new CsProjProtobufBuilder(project)
                .RemoveAllProtobuf()
                .SetProtobufType(ProtobufType.Server)
                .AddProtobufItemGroup(filePaths)
                .SaveProject();
        }

        private static MBApiMethodDefinition Define(FieldInfo delegateFieldInfo)
        {
            var name = delegateFieldInfo.Name;

            var (returnParameter, parameters) = GetDelegateFieldParameters(delegateFieldInfo);

            var inputParameters = parameters
                .Where(x => !x.IsOut)
                .Select(Define)
                .ToReadOnlyCollection();

            var outputParameters = parameters
                .Where(x => x.IsOut)
                .Select(Define)
                .ToReadOnlyCollection();

            return new MBApiMethodDefinition(name, inputParameters, outputParameters, returnParameter.ParameterType);
        }

        private static (ParameterInfo returnParameter, ParameterInfo[] parameters) GetDelegateFieldParameters(
            FieldInfo delegateFieldInfo)
        {
            var invokeMethod = delegateFieldInfo.FieldType.GetMethod("Invoke");
            if (invokeMethod is null)
            {
                throw new Exception($"Invoke method of delegate field {delegateFieldInfo.Name} is null.");
            }

            return (invokeMethod.ReturnParameter, invokeMethod.GetParameters());
        }

        private static MBApiParameterDefinition Define(ParameterInfo parameterInfo)
        {
            return new MBApiParameterDefinition(parameterInfo.ParameterType, parameterInfo.Name);
        }
    }
}