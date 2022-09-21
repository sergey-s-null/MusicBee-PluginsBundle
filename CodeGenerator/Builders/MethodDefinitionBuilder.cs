using System;
using System.Linq;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;
using CodeGenerator.Models.Abstract;

namespace CodeGenerator.Builders
{
    public class MethodDefinitionBuilder : IMethodDefinitionBuilder
    {
        public string GetClearMethodDefinition(MBApiMethodDefinition method)
        {
            var inParams = method.InputParameters
                .Select(x => $"{GetCsTypeName(x)} {x.Name}");
            var outParams = method.OutputParameters
                .Select(x => $"out {GetCsTypeName(x)} {x.Name}");

            var paramsString = string.Join(", ", inParams.Concat(outParams));

            return $"{GetCsTypeName(method.ReturnParameter)} {method.Name}({paramsString})";
        }

        private static string GetCsTypeName(IParameterType parameter)
        {
            var csTypeName = GetCsTypeName(parameter.Type);
            if (parameter.IsNullable)
            {
                csTypeName += "?";
            }

            return csTypeName;
        }

        private static string GetCsTypeName(Type type)
        {
            if (type.IsArray && type.HasElementType)
            {
                return $"{GetCsTypeName(type.GetElementType()!)}[]";
            }

            return type.GetFixedName();
        }

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