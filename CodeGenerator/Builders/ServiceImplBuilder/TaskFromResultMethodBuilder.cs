using System.Collections.Generic;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.ServiceImplBuilder
{
    public class TaskFromResultMethodBuilder : IMethodBuilder
    {
        private readonly IServiceBuilderParameters _parameters;
        private readonly IMessageTypesBuilder _messageTypesBuilder;
        private readonly ICommonLinesBuilder _commonLinesBuilder;

        public TaskFromResultMethodBuilder(
            IServiceBuilderParameters parameters, 
            IMessageTypesBuilder messageTypesBuilder,
            ICommonLinesBuilder commonLinesBuilder)
        {
            _parameters = parameters;
            _messageTypesBuilder = messageTypesBuilder;
            _commonLinesBuilder = commonLinesBuilder;
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
                    .GetResponseAssignmentLine(method.ReturnType, _parameters.ReturnParameterName)
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