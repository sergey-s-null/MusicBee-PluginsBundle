using System;
using System.Linq;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;
using Root.Helpers;

namespace CodeGenerator.Builders.ServiceImplBuilder
{
    public sealed class CommonLinesBuilder : ICommonLinesBuilder
    {
        private readonly IServiceBuilderParameters _parameters;
        private readonly IMessageTypesBuilder _messageTypesBuilder;

        public CommonLinesBuilder(
            IServiceBuilderParameters parameters,
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

            var toArrayPostfix = string.Empty;
            if (parameter.Type.IsEnumerable(out var elementType))
            {
                toArrayPostfix = elementType == typeof(byte)
                    ? ".ToByteArray()"
                    : ".ToArray()";
            }

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
            }

            return string.Empty;
        }

        public string GetResponseAssignmentLine(MBApiReturnParameterDefinition parameter, string fieldName)
        {
            var rightPart = GetResponseAssignmentLineRightPart(
                parameter.Type,
                parameter.IsNullable,
                fieldName
            );
            return $"{fieldName.Capitalize()} = {rightPart},";
        }

        public string GetResponseAssignmentLine(MBApiParameterDefinition parameter)
        {
            var rightPart = GetResponseAssignmentLineRightPart(
                parameter.Type,
                parameter.IsNullable,
                parameter.Name
            );
            return $"{parameter.Name.Capitalize()} = {rightPart},";
        }

        private static string GetResponseAssignmentLineRightPart(
            Type parameterType,
            bool isNullableType,
            string parameterName)
        {
            if (parameterType.IsEnumerable() && parameterType.HasElementType)
            {
                var elementType = parameterType.GetElementType();
                if (elementType is null)
                {
                    throw new NotSupportedException("Could not generate code for enumerable without element type.");
                }

                var nullablePart = isNullableType
                    ? $" ?? Array.Empty<{elementType.GetFixedName()}>()"
                    : string.Empty;

                return elementType == typeof(byte)
                    ? $"ByteString.CopyFrom({parameterName}{nullablePart})"
                    : $"{{ {parameterName}{nullablePart} }}";
            }

            if (parameterType.IsEnum)
            {
                return $"(int){parameterName}";
            }

            return parameterName;
        }
    }
}