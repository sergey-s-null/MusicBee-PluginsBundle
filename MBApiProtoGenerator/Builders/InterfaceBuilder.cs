using System.Collections.Generic;
using System.Linq;
using MBApiProtoGenerator.Builders.Abstract;
using MBApiProtoGenerator.Helpers;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders
{
    public class InterfaceBuilder : IInterfaceBuilder
    {
        private readonly IMethodDefinitionBuilder _methodDefinitionBuilder;
        
        public IReadOnlyCollection<string>? ImportNamespaces { get; set; }
        public string Namespace { get; set; } = "UnknownNamespace";
        public string Name { get; set; } = "InterfaceName";
        public string? BaseInterface { get; set; }

        public InterfaceBuilder(IMethodDefinitionBuilder methodDefinitionBuilder)
        {
            _methodDefinitionBuilder = methodDefinitionBuilder;
        }

        public IEnumerable<string> GenerateInterfaceLines(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            return GetImportLinesOrEmpty()
                .Concat(
                    CodeGenerationHelper.WrapWithNamespace(
                        WrapWithInterfaceDefinition(
                            GetInterfaceLines(methods)
                        ),
                        Namespace
                    )
                );
        }

        private IEnumerable<string> GetImportLinesOrEmpty()
        {
            return ImportNamespaces?
                       .Select(x => $"using {x};")
                       .Append(string.Empty)
                   ?? Enumerable.Empty<string>();
        }

        private IEnumerable<string> WrapWithInterfaceDefinition(IEnumerable<string> lines)
        {
            var basePart = BaseInterface is not null
                ? $" : {BaseInterface}"
                : string.Empty;
            var headerLine =  $"public interface {Name}{basePart}";
            return CodeGenerationHelper.WrapBlock(lines, headerLine);
        }

        private IEnumerable<string> GetInterfaceLines(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            return methods
                .Select(x => _methodDefinitionBuilder.GetClearMethodDefinition(x) + ";");
        }
    }
}