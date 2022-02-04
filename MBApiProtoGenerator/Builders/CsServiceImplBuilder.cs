using System;
using System.Collections.Generic;
using System.Linq;
using MBApiProtoGenerator.Helpers;
using MBApiProtoGenerator.Models;
using Root.Helpers;

namespace MBApiProtoGenerator.Builders
{
    public class CsServiceImplBuilder
    {
        private static readonly IReadOnlyCollection<string> UsingBlock = new[]
        {
            "using System.Linq;",
            "using System.Threading.Tasks;",
            "using Google.Protobuf.WellKnownTypes;",
            "using Grpc.Core;",
            "using Root;",
        };

        private const string RequestPostfix = "_Request";
        private const string ResponsePostfix = "_Response";
        private const string EmptyMessageType = "Empty";
        private const string ReturnParameterName = "result";

        private readonly IReadOnlyCollection<MBApiMethodDefinition> _methods;

        public CsServiceImplBuilder(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            _methods = methods;
        }
        
        public IEnumerable<string> GenerateLines()
        {
            return UsingBlock
                .Append(string.Empty)
                .Concat(GetNameSpaceLines());
        }
        
        private IEnumerable<string> GetNameSpaceLines()
        {
            yield return "namespace Module.RemoteMusicBeeApi";
            yield return "{";
            var classLines = GetClassLines();
            foreach (var classLine in classLines)
            {
                yield return classLine.Indented();
            }
            yield return "}";
        }
        
        private IEnumerable<string> GetClassLines()
        {
            yield return "public class MusicBeeApiServiceImpl : MusicBeeApiService.MusicBeeApiServiceBase";
            yield return "{";
            var classDefinitionLines = GetClassDefinitionLines();
            foreach (var classDefinitionLine in classDefinitionLines)
            {
                yield return classDefinitionLine.Indented();
            }
            yield return "}";
        }
        
        private IEnumerable<string> GetClassDefinitionLines()
        {
            yield return "private readonly MusicBeeApiInterface _mbApi;";
            yield return string.Empty;
            yield return "public MusicBeeApiServiceImpl(MusicBeeApiInterface mbApi)";
            yield return "{";
            yield return "_mbApi = mbApi;".Indented();
            yield return "}";
            yield return string.Empty;
            var methodsLines = _methods
                .Select(x => GetMethodLines(x).Append(string.Empty))
                .SelectMany(x => x);
            foreach (var methodsLine in methodsLines)
            {
                yield return methodsLine;
            }
        }

        private static IEnumerable<string> GetMethodLines(MBApiMethodDefinition method)
        {
            yield return $"public override Task<{GetResponseMessageType(method)}> " +
                         $"{method.Name}({GetRequestMessageType(method)} request, ServerCallContext context)";
            yield return "{";
            yield return "return Task.Run(() =>".Indented();
            yield return "{{".Indented();
            var methodCoreLines = Enumerable.Empty<string>()
                .Append(GetCallLine(method))
                .Concat(GetReturnLines(method));
            foreach (var methodCoreLine in methodCoreLines)
            {
                yield return methodCoreLine.Indented(2);
            }
            yield return "}});".Indented();
            yield return "}";
        }

        private static string GetCallLine(MBApiMethodDefinition method)
        {
            var resultPart = method.HasReturnType()
                ? $"var {ReturnParameterName} = "
                : string.Empty;

            var inputArguments = method.InputParameters
                .Select(GetCallInputParameter);
            var outputArguments = method.OutputParameters
                .Select(x => $"out var {x.Name}");
            var argumentsPart = string.Join(", ", inputArguments.Concat(outputArguments));

            return $"{resultPart}_mbApi.{method.Name}({argumentsPart});";
        }

        private static string GetCallInputParameter(MBApiParameterDefinition parameter)
        {
            var castPrefix = parameter.Type.IsEnum
                ? $"({parameter.Type.Name})"
                : string.Empty;
            var castPostfix = GetCastPostfix(parameter);
            var toArrayPostfix = parameter.Type.IsArray
                ? ".ToArray()"
                : string.Empty;

            return $"{castPrefix}request.{parameter.Name.Capitalize()}{castPostfix}{toArrayPostfix}";
        }

        private static string GetCastPostfix(MBApiParameterDefinition parameter)
        {
            if (parameter.Type.IsArray && parameter.Type.HasElementType)
            {
                var elementType = parameter.Type.GetElementType()!;
                if (elementType.IsEnum)
                {
                    return $".Cast<{elementType.Name}>()";
                }

                if (elementType == typeof(byte))
                {
                    return ".Select(x => (byte)x)";
                }
            }

            return string.Empty;
        }

        private static IEnumerable<string> GetReturnLines(MBApiMethodDefinition method)
        {
            if (!method.HasAnyOutputParameters())
            {
                yield return "return new Empty();";
                yield break;
            }

            yield return $"return new {GetResponseMessageType(method)}()";
            yield return "{";
            if (method.HasReturnType())
            {
                yield return GetReturnResponseLine(method.ReturnType, ReturnParameterName).Indented();
            }
            foreach (var outputParameter in method.OutputParameters)
            {
                yield return GetReturnResponseLine(outputParameter).Indented();
            }
            yield return "};";
        }
        
        private static string GetReturnResponseLine(MBApiParameterDefinition parameter)
        {
            return GetReturnResponseLine(parameter.Type, parameter.Name);
        }
        
        private static string GetReturnResponseLine(Type parameterType, string parameterName)
        {
            string rightPart;
            if (parameterType.IsEnumerable() && parameterType.HasElementType)
            {
                var elementType = parameterType.GetElementType();
                rightPart = elementType == typeof(byte)
                    ? $"{{ {parameterName}.Select(x => (int)x) }}"
                    : $"{{ {parameterName} }}";
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
        
        private static string GetRequestMessageType(MBApiMethodDefinition method)
        {
            return method.HasInputParameters()
                ? $"{method.Name}{RequestPostfix}"
                : EmptyMessageType;
        }

        private static string GetResponseMessageType(MBApiMethodDefinition method)
        {
            return method.HasAnyOutputParameters()
                ? $"{method.Name}{ResponsePostfix}"
                : EmptyMessageType;
        }
    }
}