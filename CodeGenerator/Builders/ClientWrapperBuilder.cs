using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;
using Root.Helpers;

namespace CodeGenerator.Builders
{
    public class ClientWrapperBuilder : IClientWrapperBuilder
    {
        private static readonly IReadOnlyCollection<string> ClientWrapperUsingLines = new[]
        {
            "using System.Linq;",
            "using Google.Protobuf;",
            "using Google.Protobuf.WellKnownTypes;",
            "using Root.MusicBeeApi;",
            "using Root.MusicBeeApi.Abstract;",
        };

        private readonly IMethodDefinitionBuilder _methodDefinitionBuilder;
        private readonly IMessageTypesBuilder _messageTypesBuilder;

        public string ReturnVariableName { get; set; } = "res";

        public ClientWrapperBuilder(
            IMethodDefinitionBuilder methodDefinitionBuilder,
            IMessageTypesBuilder messageTypesBuilder)
        {
            _methodDefinitionBuilder = methodDefinitionBuilder;
            _messageTypesBuilder = messageTypesBuilder;
        }

        public IEnumerable<string> GenerateClientWrapperLines(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            return ClientWrapperUsingLines
                .Append(string.Empty)
                .Concat(
                    CodeGenerationHelper.WrapWithNamespace(
                        WrapWithClassDefinition(
                            GetClassContentLines(methods)
                        ),
                        "ConsoleTests.Services"
                    )
                );
        }

        private IEnumerable<string> WrapWithClassDefinition(IEnumerable<string> lines)
        {
            return CodeGenerationHelper.WrapBlock(
                lines,
                "public class MusicBeeApiClientWrapper : IBaseMusicBeeApi"
            );
        }

        private IEnumerable<string> GetClassContentLines(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            return GetFieldsLines()
                .Append(string.Empty)
                .Concat(GetConstructorLines())
                .Append(string.Empty)
                .Concat(GetMethodsLines(methods));
        }

        private IEnumerable<string> GetFieldsLines()
        {
            yield return "private readonly MusicBeeApiService.MusicBeeApiServiceClient _client;";
        }

        private IEnumerable<string> GetConstructorLines()
        {
            yield return "public MusicBeeApiClientWrapper(MusicBeeApiService.MusicBeeApiServiceClient client)";
            yield return "{";
            yield return "_client = client;".Indented();
            yield return "}";
        }

        private IEnumerable<string> GetMethodsLines(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            return methods
                .Select(x =>
                    CodeGenerationHelper.WrapBlock(
                            GetMethodLines(x),
                            $"public {_methodDefinitionBuilder.GetClearMethodDefinition(x)}"
                        )
                        .Append(string.Empty))
                .SelectMany(x => x);
        }

        private IEnumerable<string> GetMethodLines(MBApiMethodDefinition method)
        {
            return GetClientCallLines(method)
                .Concat(GetOutParametersAssignmentLines(method))
                .Concat(GetReturnLine(method));
        }

        private IEnumerable<string> GetClientCallLines(MBApiMethodDefinition method)
        {
            var prefix = method.HasAnyOutputParameters()
                ? "var response = "
                : string.Empty;
            var postfix = method.HasInputParameters()
                ? string.Empty
                : "());";
            yield return
                $"{prefix}_client.{method.Name}(new {_messageTypesBuilder.GetRequestMessageType(method)}{postfix}";

            if (method.HasInputParameters())
            {
                yield return "{";

                foreach (var parameter in method.InputParameters)
                {
                    yield return GetRequestPropertyAssignmentLine(parameter).Indented();
                }

                yield return "});";
            }
        }

        private static string GetRequestPropertyAssignmentLine(MBApiParameterDefinition parameter)
        {
            return GetRequestPropertyAssignmentLine(parameter.Type, parameter.Name);
        }

        private static string GetRequestPropertyAssignmentLine(Type parameterType, string parameterName)
        {
            string rightPart;
            if (parameterType.IsEnumerable() && parameterType.HasElementType)
            {
                var elementType = parameterType.GetElementType()!;
                if (elementType == typeof(byte))
                {
                    rightPart = $"ByteString.CopyFrom({parameterName})";
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

        private IEnumerable<string> GetOutParametersAssignmentLines(MBApiMethodDefinition method)
        {
            return method.OutputParameters
                .Select(GetOutParameterAssignmentLine);
        }

        private static string GetOutParameterAssignmentLine(MBApiParameterDefinition parameter)
        {
            var builder = new StringBuilder();

            builder.Append(parameter.Name);
            builder.Append(" = ");

            if (parameter.Type.IsEnum)
            {
                builder.Append($"({parameter.Type.Name})");
            }

            builder.Append($"response.{parameter.Name.Capitalize()}");

            if (parameter.Type.IsArray)
            {
                if (parameter.Type.HasElementType
                    && parameter.Type.GetElementType()! == typeof(byte))
                {
                    builder.Append(".ToByteArray()");
                }
                else
                {
                    builder.Append(".ToArray()");
                }
            }

            builder.Append(";");

            return builder.ToString();
        }

        private IEnumerable<string> GetReturnLine(MBApiMethodDefinition method)
        {
            if (!method.HasReturnType())
            {
                yield break;
            }

            var enumCastPrefix = method.ReturnParameter.Type.IsEnum
                ? $"({method.ReturnParameter.Type.Name})"
                : string.Empty;
            yield return $"return {enumCastPrefix}response.{ReturnVariableName.Capitalize()};";
        }
    }
}