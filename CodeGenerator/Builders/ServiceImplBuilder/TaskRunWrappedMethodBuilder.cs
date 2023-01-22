using System.Collections.Generic;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.ServiceImplBuilder
{
    public sealed class TaskRunWrappedMethodBuilder : IMethodBuilder
    {
        private readonly IServiceBuilderParameters _parameters;
        private readonly IMessageTypesBuilder _messageTypesBuilder;

        public TaskRunWrappedMethodBuilder(
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
                WrapWithTaskRun(
                    GetMethodCoreLines(method)
                )
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

        private IEnumerable<string> WrapWithTaskRun(IEnumerable<string> lines)
        {
            yield return "return Task.Run(() =>";
            yield return "{";
            foreach (var line in lines)
            {
                yield return line.Indented();
            }

            yield return "});";
        }

        private IEnumerable<string> GetMethodCoreLines(MBApiMethodDefinition method)
        {
            yield return _commonLinesBuilder.GetMBApiCallLine(method);
            foreach (var returnLine in GetReturnLines(method))
            {
                yield return returnLine;
            }
        }

        private IEnumerable<string> GetReturnLines(MBApiMethodDefinition method)
        {
            if (!method.HasAnyOutputParameters())
            {
                yield return "return new Empty();";
                yield break;
            }

            yield return $"return new {_messageTypesBuilder.GetResponseMessageType(method)}()";
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

            yield return "};";
        }
    }
}