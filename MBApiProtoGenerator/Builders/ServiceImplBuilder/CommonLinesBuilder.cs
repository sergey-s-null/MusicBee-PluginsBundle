using System;
using System.Linq;
using MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract;
using MBApiProtoGenerator.Helpers;
using MBApiProtoGenerator.Models;
using Root.Helpers;

namespace MBApiProtoGenerator.Builders.ServiceImplBuilder
{
    public class CommonLinesBuilder : ICommonLinesBuilder
    {
        private readonly IParameters _parameters;
        private readonly IMessageTypesBuilder _messageTypesBuilder;

        public CommonLinesBuilder(
            IParameters parameters,
            IMessageTypesBuilder messageTypesBuilder)
        {
            _parameters = parameters;
            _messageTypesBuilder = messageTypesBuilder;
        }

        public string GetMethodDefinitionLine(MBApiMethodDefinition method)
        {
            return $"public override Task<{_messageTypesBuilder.GetResponseMessageType(method)}> " +
                   $"{method.Name}({_messageTypesBuilder.GetRequestMessageType(method)} request, ServerCallContext context)";
        }
        
        public string GetMBApiCallLine(MBApiMethodDefinition method)
        {
            var resultPart = method.HasReturnType()
                ? $"var {_parameters.ReturnParameterName} = "
                : string.Empty;

            var inputArguments = method.InputParameters
                .Select(GetCallInputParameter);
            var outputArguments = method.OutputParameters
                .Select(x => $"out var {x.Name}");
            var argumentsPart = string.Join(", ", inputArguments.Concat(outputArguments));

            return _parameters.WithDispatcher
                ? $"{resultPart}await _dispatcher.InvokeAsync(() => _{_parameters.MBApiVariableName}.{method.Name}({argumentsPart}));"
                : $"{resultPart}_{_parameters.MBApiVariableName}.{method.Name}({argumentsPart});";
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

        public string GetResponseAssignmentLine(MBApiParameterDefinition parameter)
        {
            return GetResponseAssignmentLine(parameter.Type, parameter.Name);
        }

        public string GetResponseAssignmentLine(Type parameterType, string parameterName)
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
    }
}