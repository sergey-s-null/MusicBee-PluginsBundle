using System;
using System.Linq;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;
using CodeGenerator.Models.Abstract;

namespace CodeGenerator.Builders
{
    public sealed class MethodDefinitionBuilder : IMethodDefinitionBuilder
    {
        public string GetClearMethodCall(MBApiMethodDefinition method, bool withVars)
        {
            var inputParameters = method.InputParameters
                .Select(x => x.Name);
            var varPart = withVars
                ? "var "
                : string.Empty;
            var outputParameters = method.OutputParameters
                .Select(x => $"out {varPart}{x.Name}");

            var parameters = string.Join(", ", inputParameters.Concat(outputParameters));

            return $"{method.Name}({parameters})";
        }
    }
}