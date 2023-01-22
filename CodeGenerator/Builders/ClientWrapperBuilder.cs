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
    public sealed class ClientWrapperBuilder : IClientWrapperBuilder
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
    }
}