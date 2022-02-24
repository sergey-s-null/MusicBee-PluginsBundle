using System.Collections.Generic;
using System.Linq;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;

namespace CodeGenerator.Builders
{
    public class MemoryContainerWrapperBuilder : IMemoryContainerWrapperBuilder
    {
        private static readonly IReadOnlyCollection<string> UsingLines = new[]
        {
            "using System;",
            "using System.Drawing;",
            "using System.Threading;",
            "using System.Windows.Forms;",
            "using Root.MusicBeeApi.Abstract;",
        };

        private readonly IMethodDefinitionBuilder _methodDefinitionBuilder;

        public string Namespace { get; set; } = "UnknownNamespace";

        public MemoryContainerWrapperBuilder(IMethodDefinitionBuilder methodDefinitionBuilder)
        {
            _methodDefinitionBuilder = methodDefinitionBuilder;
        }

        public IEnumerable<string> GenerateMemoryContainerWrapperLines(
            IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            return UsingLines
                .Append(string.Empty)
                .Concat(
                    CodeGenerationHelper.WrapWithNamespace(
                        WrapWithClassDefinition(
                            GetClassContent(methods)
                        ),
                        Namespace
                    )
                );
        }

        private IEnumerable<string> WrapWithClassDefinition(IEnumerable<string> lines)
        {
            return CodeGenerationHelper.WrapBlock(
                lines,
                "public class MusicBeeApiMemoryContainerWrapper : IMusicBeeApi"
            );
        }

        private IEnumerable<string> GetClassContent(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            return GetFieldsLines()
                .Append(string.Empty)
                .Concat(GetConstructorLines())
                .Append(string.Empty)
                .Concat(GetMethodsLines(methods));
        }

        private IEnumerable<string> GetFieldsLines()
        {
            yield return "private readonly MusicBeeApiMemoryContainer _mbApi;";
        }

        private IEnumerable<string> GetConstructorLines()
        {
            yield return "public MusicBeeApiMemoryContainerWrapper(MusicBeeApiMemoryContainer mbApi)";
            yield return "{";
            yield return "_mbApi = mbApi;".Indented();
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
                )
                .SelectMany(x => x.Append(string.Empty));
        }

        private IEnumerable<string> GetMethodLines(MBApiMethodDefinition method)
        {
            var returnPrefix = method.HasReturnType()
                ? "return "
                : string.Empty;

            yield return $"{returnPrefix}_mbApi.{_methodDefinitionBuilder.GetClearMethodCall(method, false)};";
        }
    }
}