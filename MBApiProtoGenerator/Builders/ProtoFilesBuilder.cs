using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders
{
    public class ProtoFilesBuilder
    {
        private static readonly IReadOnlyCollection<string> HeaderLines = new[]
        {
            "syntax = \"proto3\";",
            ""
        };
        
        private static readonly IReadOnlyDictionary<Type, string> TypeBindings = new Dictionary<Type, string>()
        {
            [typeof(bool)] = "bool",
            [typeof(string)] = "string",
            [typeof(int)] = "int32",
        };

        private readonly List<MBApiMethodDefinition> _methods = new();
        private string _exportPath = string.Empty;
        private string _exportPathInsideProject = string.Empty;
        private string _returnParameterName = "response";
        private string _requestPostfix = "_Req";
        private string _responsePostfix = "_Res";

        public ProtoFilesBuilder AddMethods(IEnumerable<MBApiMethodDefinition> methods)
        {
            _methods.AddRange(methods);
            return this;
        }

        public ProtoFilesBuilder SetExportPath(string projectPath, string pathInsideProject)
        {
            _exportPath = Path.Combine(projectPath, pathInsideProject);
            _exportPathInsideProject = pathInsideProject;
            return this;
        }

        public ProtoFilesBuilder SetReturnParameterName(string returnParameterName)
        {
            _returnParameterName = returnParameterName;
            return this;
        }

        public ProtoFilesBuilder SetPostfixes(string requestPostfix, string responsePostfix)
        {
            _requestPostfix = requestPostfix;
            _responsePostfix = responsePostfix;
            return this;
        }

        public ProtoFilesBuilder CreateMessagesProtoFiles()
        {
            foreach (var methodDefinition in _methods)
            {
                CreateMessagesProtoFile(methodDefinition);
            }

            return this;
        }

        private void CreateMessagesProtoFile(MBApiMethodDefinition methodDefinition)
        {
            var filePath = GetFilePath($"{methodDefinition.Name}.proto");

            var lines = HeaderLines
                .Concat(ToRequestLines(methodDefinition.Name, methodDefinition.InputParameters))
                .Concat(ToResponseLines(methodDefinition.Name, methodDefinition.OutputParameters,
                    methodDefinition.ReturnType));

            File.WriteAllLines(filePath, lines);
        }
        
        private IEnumerable<string> ToRequestLines(string methodName,
            IEnumerable<MBApiParameterDefinition> inputParameters)
        {
            yield return $"message {methodName}{_requestPostfix} {{";

            var parameterLines = inputParameters
                .Select((x, i) => $"  {GetProtoType(x.Type)} {x.Name} = {i + 1};");
            foreach (var parameterLine in parameterLines)
            {
                yield return parameterLine;
            }

            yield return "}";
            yield return string.Empty;
        }
        
        private IEnumerable<string> ToResponseLines(string methodName,
            IEnumerable<MBApiParameterDefinition> outputParameters, Type returnType)
        {
            yield return $"message {methodName}{_responsePostfix} {{";

            yield return $"  {GetProtoType(returnType)} {_returnParameterName} = 1;";
            
            var parameterLines = outputParameters
                .Select((x, i) => $"  {GetProtoType(x.Type)} {x.Name} = {i + 2};");
            foreach (var parameterLine in parameterLines)
            {
                yield return parameterLine;
            }

            yield return "}";
            yield return string.Empty;
        }

        public ProtoFilesBuilder CreateServiceProtoFile(string serviceName)
        {
            var filePath = GetFilePath($"{serviceName}.proto");

            var lines = HeaderLines
                .Concat(GetServiceImportLines())
                .Append(string.Empty)
                .Concat(GetServiceLines(serviceName));
            
            File.WriteAllLines(filePath, lines);
            
            return this;
        }
        
        private IEnumerable<string> GetServiceImportLines()
        {
            return _methods
                .Select(methodDefinition => $"import \"{_exportPathInsideProject}/{methodDefinition.Name}.proto\";");
        }
        
        private IEnumerable<string> GetServiceLines(string serviceName)
        {
            yield return $"service {serviceName} {{";
            
            foreach (var methodDefinition in _methods)
            {
                var methodName = methodDefinition.Name;
                var requestMessageName = $"{methodName}{_requestPostfix}";
                var responseMessageName = $"{methodName}{_responsePostfix}";
                yield return $"  rpc {methodName}({requestMessageName})";
                yield return $"      returns({responseMessageName});";
            }

            yield return "}";
        }

        private string GetFilePath(string fileName)
        {
            return !string.IsNullOrEmpty(_exportPath)
                ? Path.Combine(_exportPath, fileName)
                : fileName;
        }
        
        private static string GetProtoType(Type parameterType, bool withEnumerable = true)
        {
            if (withEnumerable
                && IsEnumerable(parameterType, out var argumentType))
            {
                var argumentProtoType = GetProtoType(argumentType!, false);
                return $"repeated {argumentProtoType}";
            }

            if (parameterType.IsEnum
                && TypeBindings.TryGetValue(typeof(int), out var intProtoType))
            {
                return intProtoType;
            }

            if (TypeBindings.TryGetValue(typeof(int), out var protoType))
            {
                return protoType;
            }

            throw new Exception($"Для типа {parameterType.FullName} не найдено соответствие proto типа.");
        }
        
        private static bool IsEnumerable(Type type, out Type? argumentType)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition().IsAssignableFrom(typeof(IEnumerable<>)))
            {
                argumentType = type.GenericTypeArguments.First();
                return true;
            }

            argumentType = null;
            return false;
        }
    }
}