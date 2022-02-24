using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Helpers;
using CodeGenerator.Models;

namespace CodeGenerator.Builders
{
    public class MessageTypesBuilder : IMessageTypesBuilder
    {
        private readonly IServiceBuilderParameters _parameters;

        public MessageTypesBuilder(IServiceBuilderParameters parameters)
        {
            _parameters = parameters;
        }

        public string GetRequestMessageType(MBApiMethodDefinition method)
        {
            return method.HasInputParameters()
                ? $"{method.Name}{_parameters.RequestPostfix}"
                : _parameters.EmptyMessageType;
        }

        public string GetResponseMessageType(MBApiMethodDefinition method)
        {
            return method.HasAnyOutputParameters()
                ? $"{method.Name}{_parameters.ResponsePostfix}"
                : _parameters.EmptyMessageType;
        }
    }
}