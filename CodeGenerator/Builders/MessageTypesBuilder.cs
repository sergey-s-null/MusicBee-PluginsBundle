using MBApiProtoGenerator.Builders.Abstract;
using MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract;
using MBApiProtoGenerator.Helpers;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders
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