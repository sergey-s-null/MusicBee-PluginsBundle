using System;
using System.Collections.Generic;
using System.Linq;
using MBApiProtoGenerator.Helpers;
using MBApiProtoGenerator.Models;
using Root.Helpers;

namespace MBApiProtoGenerator.Builders
{
    public class CsClientServiceBuilder
    {
        private static readonly IReadOnlyDictionary<Type, string> BaseTypesMappings = new Dictionary<Type, string>
        {
            [typeof(string)] = "string",
            [typeof(bool)] = "bool",
            [typeof(void)] = "void",
            [typeof(int)] = "int",
            [typeof(byte)] = "byte",
            [typeof(float)] = "float",
            [typeof(double)] = "double",
        };

        private static readonly IReadOnlyCollection<string> InterfaceUsingLines = new[]
        {
            "using Root;"
        };
        
        private static readonly IReadOnlyCollection<string> ClientWrapperUsingLines = new[]
        {
            "using System.Linq;",
            "using Google.Protobuf.WellKnownTypes;",
            "using Root;"
        };

        private const string RequestPostfix = "_Request";
        private const string EmptyMessageType = "Empty";
        private const string ReturnVariableName = "result";

        private readonly IReadOnlyCollection<MBApiMethodDefinition> _methods;

        public CsClientServiceBuilder(IReadOnlyCollection<MBApiMethodDefinition> method)
        {
            _methods = method;
        }

        public IEnumerable<string> GenerateInterfaceLines()
        {
            foreach (var usingLine in InterfaceUsingLines)
            {
                yield return usingLine;
            }
            yield return string.Empty;
            yield return "namespace ConsoleTests.Services";
            yield return "{";
            foreach (var interfaceLine in GetInterfaceLines())
            {
                yield return interfaceLine.Indented();
            }
            yield return "}";
        }

        private IEnumerable<string> GetInterfaceLines()
        {
            yield return "public interface IMusicBeeApi";
            yield return "{";
            var methodsLines = _methods
                .Select(x => GetClearMethodLine(x) + ";");
            foreach (var methodsLine in methodsLines)
            {
                yield return methodsLine.Indented();
            }
            yield return "}";
        }
        
        private static string GetCsTypeName(Type type)
        {
            if (type.IsArray && type.HasElementType)
            {
                return $"{GetCsTypeName(type.GetElementType()!)}[]";
            }
            
            return BaseTypesMappings.TryGetValue(type, out var stringType) 
                ? stringType 
                : type.Name;
        }

        public IEnumerable<string> GenerateClientWrapperLines()
        {
            foreach (var usingLine in ClientWrapperUsingLines)
            {
                yield return usingLine;
            }
            yield return string.Empty;
            yield return "namespace ConsoleTests.Services";
            yield return "{";
            foreach (var clientWrapperLine in GetClientWrapperLines())
            {
                yield return clientWrapperLine.Indented();
            }
            yield return "}";
        }

        private IEnumerable<string> GetClientWrapperLines()
        {
            yield return "public class MusicBeeApiClientWrapper : IMusicBeeApi";
            yield return "{";
            foreach (var clientWrapperContentLine in GetClientWrapperContentLines())
            {
                yield return clientWrapperContentLine.Indented();
            }
            yield return "}";
        }

        private IEnumerable<string> GetClientWrapperContentLines()
        {
            yield return "private readonly MusicBeeApiService.MusicBeeApiServiceClient _client;";
            yield return string.Empty;
            yield return "public MusicBeeApiClientWrapper(MusicBeeApiService.MusicBeeApiServiceClient client)";
            yield return "{";
            yield return "_client = client;".Indented();
            yield return "}";
            yield return string.Empty;
            var methodsLines = _methods
                .Select(x => GetClientWrapperMethodLines(x).Append(string.Empty))
                .SelectMany(x => x);
            foreach (var methodsLine in methodsLines)
            {
                yield return methodsLine;
            }
        }

        private IEnumerable<string> GetClientWrapperMethodLines(MBApiMethodDefinition method)
        {
            yield return $"public {GetClearMethodLine(method)}";
            yield return "{";
            foreach (var contentLine in GetClientWrapperMethodContentLines(method))
            {
                yield return contentLine.Indented();
            }
            yield return "}";
        }

        private IEnumerable<string> GetClientWrapperMethodContentLines(MBApiMethodDefinition method)
        {
            var prefix = method.HasAnyOutputParameters()
                ? "var response = "
                : string.Empty;
            var postfix = method.HasInputParameters()
                ? string.Empty
                : "());";
            
            yield return $"{prefix}_client.{method.Name}(new {GetRequestMessageType(method)}{postfix}";
            if (method.HasInputParameters())
            {
                yield return "{";
                foreach (var parameter in method.InputParameters)
                {
                    yield return GetRequestParameterLine(parameter).Indented();
                }
                yield return "});";
            }
            foreach (var parameter in method.OutputParameters)
            {
                yield return GetResponseParameterLine(parameter);
            }

            if (method.HasReturnType())
            {
                var enumCastPrefix = method.ReturnType.IsEnum
                    ? $"({method.ReturnType.Name})"
                    : string.Empty;
                yield return $"return {enumCastPrefix}response.{ReturnVariableName.Capitalize()};";
            }
        }

        private static string GetRequestParameterLine(MBApiParameterDefinition parameter)
        {
            return GetRequestParameterLine(parameter.Type, parameter.Name);
        }

        private static string GetRequestParameterLine(Type parameterType, string parameterName)
        {
            string rightPart;
            if (parameterType.IsEnumerable() && parameterType.HasElementType)
            {
                var elementType = parameterType.GetElementType()!;
                if (elementType == typeof(byte))
                {
                    rightPart = $"{{ {parameterName}.Select(x => (int)x) }}";
                }
                else if (elementType.IsEnum)
                {
                    rightPart = $"{{ {parameterName}.Cast<int>() }}";
                }
                else
                {
                    rightPart = $"{{ {parameterName} }}";
                }
            }
            else if (parameterType.IsEnum)
            {
                rightPart = $"(int){parameterName}";
            }
            else
            {
                rightPart = parameterName;
            }
            
            return $"{parameterName.Capitalize()} = {rightPart},";
        }

        private static string GetResponseParameterLine(MBApiParameterDefinition parameter)
        {
            var castPrefix = parameter.Type.IsEnum
                ? $"({parameter.Type.Name})"
                : string.Empty;
            var castPostfix = parameter.Type.IsArray
                              && parameter.Type.HasElementType
                              && parameter.Type.GetElementType()! == typeof(byte)
                ? ".Select(x => (byte)x)"
                : string.Empty;
            var toArrayPostfix = parameter.Type.IsArray
                ? ".ToArray()"
                : string.Empty;
            
            return $"{parameter.Name} = {castPrefix}response.{parameter.Name.Capitalize()}{castPostfix}{toArrayPostfix};";
        }
        
        private static string GetClearMethodLine(MBApiMethodDefinition method)
        {
            var inParams = method.InputParameters
                .Select(x => $"{GetCsTypeName(x.Type)} {x.Name}");
            var outParams = method.OutputParameters
                .Select(x => $"out {GetCsTypeName(x.Type)} {x.Name}");

            var paramsString = string.Join(", ", inParams.Concat(outParams));
            
            return $"{GetCsTypeName(method.ReturnType)} {method.Name}({paramsString})";
        }
        
        private static string GetRequestMessageType(MBApiMethodDefinition method)
        {
            return method.HasInputParameters()
                ? $"{method.Name}{RequestPostfix}"
                : EmptyMessageType;
        }
    }
}