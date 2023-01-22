using System.Collections.Generic;
using System.Linq;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.ServiceImplBuilder
{
    public sealed class ServiceBuilder : IServiceBuilder
    {
        private static readonly IReadOnlyCollection<string> UsingBlock = new[]
        {
            "using System;",
            "using System.Linq;",
            "using System.Threading.Tasks;",
            "using Google.Protobuf;",
            "using Google.Protobuf.WellKnownTypes;",
            "using Grpc.Core;",
            "using Root.MusicBeeApi;",
            "using Root.MusicBeeApi.Abstract;",
        };

        private static readonly IReadOnlyCollection<string> ResharperBlock = new[]
        {
            "// ReSharper disable ConstantNullCoalescingCondition"
        };

        private const string Namespace = "Module.RemoteMusicBeeApi";
        private const string ClassName = "MusicBeeApiServiceImpl";
        private const string ServiceName = "MusicBeeApiService";

        private readonly IServiceBuilderParameters _parameters;

        public ServiceBuilder(IServiceBuilderParameters parameters)
        {
            _parameters = parameters;
        }

        public IEnumerable<string> GenerateServiceLines(IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            var coreLines = WrapWithNamespace(
                WrapWithClassNameLine(
                    GetClassDefinitionLines(methods)
                )
            );

            return UsingBlock
                .Append(string.Empty)
                .Concat(ResharperBlock)
                .Append(string.Empty)
                .Concat(coreLines);
        }

        private static IEnumerable<string> WrapWithNamespace(IEnumerable<string> lines)
        {
            yield return $"namespace {Namespace}";
            yield return "{";
            foreach (var line in lines)
            {
                yield return line.Indented();
            }

            yield return "}";
        }

        private static IEnumerable<string> WrapWithClassNameLine(IEnumerable<string> innerClassLines)
        {
            yield return $"public class {ClassName} : {ServiceName}.{ServiceName}Base";
            yield return "{";
            foreach (var innerClassLine in innerClassLines)
            {
                yield return innerClassLine.Indented();
            }

            yield return "}";
        }

        private IEnumerable<string> GetClassDefinitionLines(IEnumerable<MBApiMethodDefinition> methods)
        {
            return GetClassFieldsLines()
                .Append(string.Empty)
                .Concat(GetConstructorLines())
                .Append(string.Empty)
                .Concat(GetMethodsLines(methods));
        }

        private static IEnumerable<string> GetClassFieldsLines()
        {
            yield return "private readonly IBaseMusicBeeApi _mbApi;";
        }

        private IEnumerable<string> GetConstructorLines()
        {
            var dispatcherPart = _parameters.WithDispatcher
                ? ", Dispatcher dispatcher"
                : string.Empty;
            yield return $"public {ClassName}(IBaseMusicBeeApi mbApi{dispatcherPart})";
            yield return "{";
            yield return "_mbApi = mbApi;".Indented();
            if (_parameters.WithDispatcher)
            {
                yield return "_dispatcher = dispatcher;".Indented();
            }

            yield return "}";
        }

        private IEnumerable<string> GetMethodsLines(IEnumerable<MBApiMethodDefinition> methods)
        {
            return methods
                .Select(x => _methodBuilder.GenerateMethodLines(x))
                .SelectMany(x => x.Append(string.Empty));
        }
    }
}