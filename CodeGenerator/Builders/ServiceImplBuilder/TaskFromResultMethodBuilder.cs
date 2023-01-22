using System.Collections.Generic;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.ServiceImplBuilder
{
    public sealed class TaskFromResultMethodBuilder : IMethodBuilder
    {
        private readonly IServiceBuilderParameters _parameters;
        private readonly IMessageTypesBuilder _messageTypesBuilder;

        public TaskFromResultMethodBuilder(
            IServiceBuilderParameters parameters, 
            IMessageTypesBuilder messageTypesBuilder)
        {
            _parameters = parameters;
            _messageTypesBuilder = messageTypesBuilder;
        }

        public IEnumerable<string> GenerateMethodLines(MBApiMethodDefinition method)
        {
            return WrapWithMethodDefinition(
                method,
                GetMethodContentLines(method)
            );
        }

        private IEnumerable<string> WrapWithMethodDefinition(MBApiMethodDefinition method, IEnumerable<string> lines)
        {
            yield return _commonLinesBuilder.GetMethodDefinitionLine(method);
            yield return "{";
            foreach (var line in lines)
            {
                yield return line.Indented();
            }

            yield return "}";
        }
        
        private IEnumerable<string> GetMethodContentLines(MBApiMethodDefinition method)
        {
            yield return _commonLinesBuilder.GetMBApiCallLine(method);

            if (!method.HasReturnType())
            {
                yield return $"return Task.FromResult(new {_parameters.EmptyMessageType}());";
                yield break;
            }

            yield return $"return Task.FromResult(new {_messageTypesBuilder.GetResponseMessageType(method)}";
            yield return "{";
            if (method.HasReturnType())
            {
                yield return _commonLinesBuilder
                    .GetResponseAssignmentLine(method.ReturnParameter, _parameters.ReturnParameterName)
                    .Indented();
            }

            foreach (var outputParameter in method.OutputParameters)
            {
                yield return _commonLinesBuilder
                    .GetResponseAssignmentLine(outputParameter)
                    .Indented();
            }

            yield return "});";
        }
    }
}