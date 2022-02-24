using System.Collections.Generic;
using MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract;
using MBApiProtoGenerator.Helpers;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.ServiceImplBuilder
{
    public class TaskRunWrappedMethodBuilder : IMethodBuilder
    {
        private readonly IParameters _parameters;
        private readonly IMessageTypesBuilder _messageTypesBuilder;
        private readonly ICommonLinesBuilder _commonLinesBuilder;

        public TaskRunWrappedMethodBuilder(
            IParameters parameters,
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
                    .GetResponseAssignmentLine(method.ReturnType, _parameters.ReturnParameterName)
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