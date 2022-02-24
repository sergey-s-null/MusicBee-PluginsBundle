using System;
using System.Collections.Generic;
using System.Linq;
using MBApiProtoGenerator.Builders.Abstract;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders
{
    public class MethodDefinitionBuilder : IMethodDefinitionBuilder
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
            [typeof(object)] = "object"
        };

        public string GetClearMethodDefinition(MBApiMethodDefinition method)
        {
            var inParams = method.InputParameters
                .Select(x => $"{GetCsTypeName(x.Type)} {x.Name}");
            var outParams = method.OutputParameters
                .Select(x => $"out {GetCsTypeName(x.Type)} {x.Name}");

            var paramsString = string.Join(", ", inParams.Concat(outParams));

            return $"{GetCsTypeName(method.ReturnType)} {method.Name}({paramsString})";
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